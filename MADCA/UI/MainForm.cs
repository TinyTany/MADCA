using MADCA.Core.Data;
using MADCA.Core.FumenData;
using MADCA.Core.Graphics;
using MADCA.Core.Note;
using MADCA.Core.Note.Abstract;
using MADCA.Core.Note.Concrete;
using MADCA.Core.Operation;
using MADCA.Core.Score;
using MADCA.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MADCA.UI
{
    public enum EditorMode
    {
        Add, Edit, Delete
    }

    public enum NoteMode
    {
        Touch, Chain, SlideL, SlideR, SnapU, SnapD, Hold, HoldRelay, Field, Bpm, Speed
    }

    public enum SelectedNoteArea
    {
        None, Left, Center, Right
    }

    public partial class MainForm : Form
    {
        private readonly string appName = "MADCA";
        public MainForm()
        {
            InitializeComponent();
            Text = appName;
            var display = new MadcaDisplay(new PictureBox() { Location = new Point(), Size = ClientSize });
            var fumen = new MadcaFumenData();
            var scoreBook = fumen.ScoreBook;
            var noteBook = fumen.NoteBook;
            var operationManager = new OperationManager();
            var status = new EditorStatus(EditorMode.Add, NoteMode.Touch, 8, new TimingPosition(8, 1));
            for (var i = 0; i < 100; ++i) { scoreBook.AddScoreLast(new Score(4, 4)); }

            SetEditorMode(EditorMode.Add, status);
            tsbAdd.Click += (s, e) => SetEditorMode(EditorMode.Add, status);
            tsbEdit.Click += (s, e) => SetEditorMode(EditorMode.Edit, status);
            tsbDelete.Click += (s, e) => SetEditorMode(EditorMode.Delete, status);

            tsbUndo.Enabled = tsbRedo.Enabled = false;
            tsbUndo.Checked = tsbRedo.Checked = false;
            tsbUndo.Click += (s, e) =>
            {
                operationManager.Undo();
                display.PictureBox.Refresh();
            };
            tsbRedo.Click += (s, e) =>
            {
                operationManager.Redo();
                display.PictureBox.Refresh();
            };
            operationManager.UndoStatusChanged += (b) => tsbUndo.Enabled = b;
            operationManager.RedoStatusChanged += (b) => tsbRedo.Enabled = b;

            SetNoteMode(NoteMode.Touch, status);
            tsbTouch.Click += (s, e) => SetNoteMode(NoteMode.Touch, status);
            tsbChain.Click += (s, e) => SetNoteMode(NoteMode.Chain, status);
            tsbSlideL.Click += (s, e) => SetNoteMode(NoteMode.SlideL, status);
            tsbSlideR.Click += (s, e) => SetNoteMode(NoteMode.SlideR, status);
            tsbSnapU.Click += (s, e) => SetNoteMode(NoteMode.SnapU, status);
            tsbSnapD.Click += (s, e) => SetNoteMode(NoteMode.SnapD, status);
            tsbHold.Click += (s, e) => SetNoteMode(NoteMode.Hold, status);
            tsbHoldStep.Click += (s, e) => SetNoteMode(NoteMode.HoldRelay, status);
            tsbField.Click += (s, e) => SetNoteMode(NoteMode.Field, status);

            // 分数指定用ComboBoxの設定
            var beatStride = new List<uint>() { 4, 8, 12, 16, 24, 32, 48, 64 };
            tscbBeat.Items.AddRange(beatStride.Select(x => $"1 / {x}").ToArray());
            tscbBeat.Items.Add("カスタム...");
            tscbBeat.SelectedIndexChanged += (s, e) =>
            {
                if (tscbBeat.SelectedIndex == tscbBeat.Items.Count - 1)
                {
                    using (var dialog = new BeatStrideDialog())
                    {
                        dialog.BeatStrideConfirmed += (_, t) =>
                        {
                            status.BeatStride = t;
                            // 反映されない...（すでに存在するものの場合は反映された）
                            tscbBeat.Text = $"{t.CntValue} / {t.DivValue}";
                        };
                        dialog.ShowDialog();
                    }
                    return;
                }
                if (tscbBeat.SelectedIndex < 0 || tscbBeat.SelectedIndex >= beatStride.Count)
                { 
                    return; 
                }
                status.BeatStride = new TimingPosition(beatStride[tscbBeat.SelectedIndex], 1);
            };
            tscbBeat.SelectedIndex = 0;

#if DEBUG
            var time = new Stopwatch();
            display.PictureBox.Paint += (s, e) => time.Restart();
#endif
            display.PictureBox.Paint += (s, e) => 
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                // まずレーンを描画
                LaneDrawer.Draw(e.Graphics, display.EditorLaneEnvironment, scoreBook.Scores);
                PreviewDrawer.Draw(e.Graphics, display.PreviewDisplayEnvironment, scoreBook.Scores);
                // 次にレーン上にノーツを描画
                NoteDrawer.DrawToLane(e.Graphics, display.EditorLaneEnvironment, noteBook);
                NoteDrawer.DrawToPreview(e.Graphics, display.PreviewDisplayEnvironment, noteBook);
            };
            
            SizeChanged += (s, e) =>
            {
                display.PictureBox.Size = ClientSize;
            };
            Controls.Add(display.PictureBox);
            MinimumSize = new Size((int)display.EditorLaneEnvironment.SideMargin * 2, 10);

            SetEventToSetCursor(display, noteBook, status);
            SetEventToPutNote(display, noteBook, scoreBook.Scores, operationManager, status);
            SetEventToPutHold(display, noteBook, scoreBook.Scores, operationManager, status);
            SetEventToPutHoldRelay(display, noteBook, scoreBook.Scores, operationManager, status);
            SetEventToReLocateNote(display, noteBook, scoreBook.Scores, operationManager, status);
            SetEventToReSizeNote(display, noteBook, scoreBook.Scores, operationManager, status);
            SetEventToDeleteNote(display, noteBook, operationManager, status);
            SetEventToDrawPreviewNote(display, scoreBook.Scores, status);
            SetEventForRefreshRule(display); // 各種SetEvent関数の最後に置く
#if DEBUG
            display.PictureBox.Paint += (s, e) => 
            {
                time.Stop();
                Console.WriteLine($"Time[ms]: {time.ElapsedMilliseconds}");
            };
#endif
        }

        private void SetEditorMode(EditorMode mode, EditorStatus status)
        {
            status.EditorMode = mode;
            switch (mode)
            {
                case EditorMode.Add:
                    {
                        tsbAdd.Checked = true;
                        tsbEdit.Checked = false;
                        tsbDelete.Checked = false;
                        return;
                    }
                case EditorMode.Edit:
                    {
                        tsbAdd.Checked = false;
                        tsbEdit.Checked = true;
                        tsbDelete.Checked = false;
                        return;
                    }
                case EditorMode.Delete:
                    {
                        tsbAdd.Checked = false;
                        tsbEdit.Checked = false;
                        tsbDelete.Checked = true;
                        return;
                    }
                default:
                    Debug.Assert(false);
                    break;
            }
        }

        private void SetNoteMode(NoteMode mode, EditorStatus status)
        {
            status.NoteMode = mode;
            foreach(var item in toolStrip2.Items)
            {
                var tsb = item as ToolStripButton;
                if (tsb != null)
                {
                    tsb.Checked = false;
                }
            }
            switch (mode)
            {
                case NoteMode.Touch:
                    tsbTouch.Checked = true;
                    return;
                case NoteMode.Chain:
                    tsbChain.Checked = true;
                    return;
                case NoteMode.SlideL:
                    tsbSlideL.Checked = true;
                    return;
                case NoteMode.SlideR:
                    tsbSlideR.Checked = true;
                    return;
                case NoteMode.SnapU:
                    tsbSnapU.Checked = true;
                    return;
                case NoteMode.SnapD:
                    tsbSnapD.Checked = true;
                    return;
                case NoteMode.Hold:
                    tsbHold.Checked = true;
                    return;
                case NoteMode.HoldRelay:
                    tsbHoldStep.Checked = true;
                    return;
                case NoteMode.Field:
                    tsbField.Checked = true;
                    return;
                default:
                    Debug.Assert(false);
                    break;
            }
        }

        #region イベント設定用関数群

        private static void SetEventToDrawPreviewNote(
            MadcaDisplay display, IReadOnlyList<IReadOnlyScore> scores, IReadOnlyEditorStatus status)
        {
            var box = display.PictureBox;
            var laneEnv = display.EditorLaneEnvironment;
            var previewEnv = display.PreviewDisplayEnvironment;
            var holder = display.KeyTokenHolder;

            var note = new PreviewNote(new LanePotision(0), new TimingPosition(1, 0), new NoteSize(status.NoteSize));
            bool visible = false;
            box.MouseMove += (s, e) =>
            {
                if (holder.Locked || status.EditorMode != EditorMode.Add)
                {
                    visible = false;
                    return;
                }
                var area = laneEnv.GetEditorLaneRegion(e.Location);
                if (area == EditorLaneRegion.Lane)
                {
                    visible = true;
                    var res = PositionConverter.ConvertRealToVirtual(laneEnv, e.Location, status.BeatStride, scores, out Position position);
                    if (res)
                    {
                        note.ReLocate(position.Lane, position.Timing);
                    }
                }
                else
                {
                    visible = false;
                }
            };
            box.Paint += (s, e) =>
            {
                if (visible)
                {
                    note.SelectedNote = status.NoteMode.ToNoteType();
                    NoteDrawer.DrawToLane(e.Graphics, laneEnv, note);
                    NoteDrawer.DrawToPreview(e.Graphics, previewEnv, note);
                }
            };
        }

        private static void SetEventToPutNote(
            MadcaDisplay display, NoteBook noteBook, IReadOnlyList<IReadOnlyScore> scores, OperationManager opManager, IReadOnlyEditorStatus status)
        {
            var box = display.PictureBox;
            var laneEnv = display.EditorLaneEnvironment;

            box.MouseDown += (s, e) =>
            {
                if (status.EditorMode != EditorMode.Add || status.NoteMode == NoteMode.Hold || status.NoteMode == NoteMode.Field) { return; }
                var area = laneEnv.GetEditorLaneRegion(e.Location);
                if (area == EditorLaneRegion.Lane && e.Button == MouseButtons.Left)
                {
                    var res = PositionConverter.ConvertRealToVirtual(laneEnv, e.Location, status.BeatStride, scores, out Position position);
                    if (!res) { return; }
                    var note = MyUtil.NoteFactory(position.Lane, position.Timing, new NoteSize(status.NoteSize), status.NoteMode);
                    if (note is null) { return; } // HACK: この辺の処理どうしようかな
                    opManager.AddAndInvokeOperation(new AddShortNoteOperation(noteBook, note));
                }
            };
        }

        private static void SetEventToPutHold(
            MadcaDisplay display, NoteBook noteBook, IReadOnlyList<IReadOnlyScore> scores, OperationManager opManager, IReadOnlyEditorStatus status)
        {
            var box = display.PictureBox;
            var env = display.EditorLaneEnvironment;
            var holder = display.KeyTokenHolder;

            var key = new KeyToken();
            HoldEnd end = null;
            box.MouseDown += (s, e) =>
            {
                if (holder.Locked) { return; }
                if (status.EditorMode != EditorMode.Add || status.NoteMode != NoteMode.Hold) { return; }
                var area = env.GetEditorLaneRegion(e.Location);
                if (area == EditorLaneRegion.Lane && e.Button == MouseButtons.Left)
                {
                    var res = PositionConverter.ConvertRealToVirtual(env, e.Location, status.BeatStride, scores, out Position position);
                    if (!res) { return; }
                    var hold = new Hold(position.Lane, position.Timing, position.Timing + status.BeatStride, new NoteSize(status.NoteSize));
                    end = hold.HoldEnd;
                    opManager.AddAndInvokeOperation(new AddHoldOperation(noteBook, hold));
                    holder.Lock(key);
                }
            };

            box.MouseMove += (s, e) =>
            {
                if (!holder.CanUnLock(key)) { return; }
                var res = PositionConverter.ConvertRealToVirtual(env, e.Location, status.BeatStride, scores, out Position position);
                if (!res) { return; }
                end.ReLocate(position.Lane, position.Timing);
            };

            box.MouseUp += (s, e) =>
            {
                if (!holder.UnLock(key)) { return; }
                end = null;
            };
        }

        private static void SetEventToPutHoldRelay(
            MadcaDisplay display, NoteBook noteBook, IReadOnlyList<IReadOnlyScore> scores, OperationManager opManager, IReadOnlyEditorStatus status)
        {
            var box = display.PictureBox;
            var env = display.EditorLaneEnvironment;
            var holder = display.KeyTokenHolder;

            var key = new KeyToken();
            HoldRelay relay = null;
            box.MouseDown += (s, e) =>
            {
                if (holder.Locked) { return; }
                if (status.EditorMode != EditorMode.Add || status.NoteMode != NoteMode.HoldRelay) { return; }
                var area = env.GetEditorLaneRegion(e.Location);
                if (area == EditorLaneRegion.Lane && e.Button == MouseButtons.Left)
                {
                    Hold hold = null;
                    foreach(var h in noteBook.Holds.Reverse())
                    {
                        if(h.Contains(e.Location, env))
                        {
                            hold = h;
                            break;
                        }
                    }
                    if (hold == null) { return; }
                    var res = PositionConverter.ConvertRealToVirtual(env, e.Location, status.BeatStride, scores, out Position position);
                    if (!res) { return; }
                    relay = new HoldRelay(position.Lane, position.Timing, new NoteSize(status.NoteSize));
                    opManager.AddAndInvokeOperation(new AddHoldRelayOperation(hold, relay));
                    holder.Lock(key);
                }
            };

            box.MouseMove += (s, e) =>
            {
                if (!holder.CanUnLock(key)) { return; }
                var res = PositionConverter.ConvertRealToVirtual(env, e.Location, status.BeatStride, scores, out Position position);
                if (!res) { return; }
                relay.ReLocate(position.Lane, position.Timing);
            };

            box.MouseUp += (s, e) =>
            {
                if (!holder.UnLock(key)) { return; }
                relay = null;
            };
        }

        private static bool GetSelectedNote(Point p, MadcaDisplay display, NoteBook noteBook, out NoteBase note)
        {
            var env = display.EditorLaneEnvironment;
            note = noteBook.Notes.FindLast(x => x.GetRectangle(env).ContainsEx(p, env));
            if (note == null)
            {
                foreach (var hold in noteBook.Holds.Reverse())
                {
                    note = hold.AllNotes.Find(x => x.GetRectangle(env).ContainsEx(p, env));
                    if (note != null)
                    {
                        break;
                    }
                }
                if (note == null)
                {
                    return false;
                }
            }
            return true;
        }

        private static void SetEventToReLocateNote(
            MadcaDisplay display, NoteBook noteBook, IReadOnlyList<IReadOnlyScore> scores, OperationManager opManager, IReadOnlyEditorStatus status)
        {
            var box = display.PictureBox;
            var env = display.EditorLaneEnvironment;
            var holder = display.KeyTokenHolder;

            var key = new KeyToken();
            NoteBase note = null;
            Position prev = null;
            Position mouseBegin = null;
            box.MouseDown += (s, e) =>
            {
                if (holder.Locked || status.EditorMode != EditorMode.Edit) { return; }
                var area = env.GetEditorLaneRegion(e.Location);
                if (area == EditorLaneRegion.Lane && e.Button == MouseButtons.Left)
                {
                    if (!GetSelectedNote(e.Location, display, noteBook, out note))
                    {
                        return;
                    }
                    if (note.SelectedNoteArea(e.Location, env) != SelectedNoteArea.Center)
                    {
                        return;
                    }
                    prev = new Position(note.Lane, note.Timing);
                    var res = PositionConverter.ConvertRealToVirtual(env, e.Location, status.BeatStride, scores, out mouseBegin);
                    if (!res)
                    {
                        return;
                    }
                    holder.Lock(key);
                }
            };

            box.MouseMove += (s, e) =>
            {
                if (!holder.CanUnLock(key)) { return; }
                var res = PositionConverter.ConvertRealToVirtual(env, e.Location, status.BeatStride, scores, out Position position);
                if (!res) { return; }
                var lane = position.Lane.RawLane - mouseBegin.Lane.RawLane;
                var timing = position.Timing - mouseBegin.Timing;
                note.ReLocate(new LanePotision(prev.Lane.RawLane + lane), prev.Timing + timing);
            };

            box.MouseUp += (s, e) =>
            {
                if (!holder.UnLock(key)) { return; }
                // TODO: 条件式要検討
                if (prev.Lane.RawLane != note.Lane.RawLane || prev.Timing != note.Timing)
                {
                    opManager.AddOperation(new ReLocateNoteOperation(note, prev, new Position(note.Lane, note.Timing)));
                }
                note = null;
            };
        }

        private static void SetEventToReSizeNote(
            MadcaDisplay display, NoteBook noteBook, IReadOnlyList<IReadOnlyScore> scores, OperationManager opManager, IReadOnlyEditorStatus status)
        {
            var box = display.PictureBox;
            var env = display.EditorLaneEnvironment;
            var holder = display.KeyTokenHolder;

            var key = new KeyToken();
            NoteBase note = null;
            Position prevPos = null;
            NoteSize prevSize = null;
            Position mouseBegin = null;
            SelectedNoteArea noteArea = SelectedNoteArea.None;
            box.MouseDown += (s, e) =>
            {
                if (holder.Locked || status.EditorMode != EditorMode.Edit) { return; }
                var area = env.GetEditorLaneRegion(e.Location);
                if (area == EditorLaneRegion.Lane && e.Button == MouseButtons.Left)
                {
                    if (!GetSelectedNote(e.Location, display, noteBook, out note))
                    {
                        return;
                    }
                    noteArea = note.SelectedNoteArea(e.Location, env);
                    if (!(noteArea == SelectedNoteArea.Left || noteArea == SelectedNoteArea.Right))
                    {
                        return;
                    }
                    prevPos = new Position(note.Lane, note.Timing);
                    prevSize = new NoteSize(note.NoteSize);
                    var res = PositionConverter.ConvertRealToVirtual(env, e.Location, status.BeatStride, scores, out mouseBegin);
                    if (!res)
                    {
                        return;
                    }
                    holder.Lock(key);
                }
            };

            box.MouseMove += (s, e) =>
            {
                if (!holder.CanUnLock(key)) { return; }
                var res = PositionConverter.ConvertRealToVirtual(env, e.Location, status.BeatStride, scores, out Position position);
                if (!res) { return; }
                var lane = position.Lane.RawLane - mouseBegin.Lane.RawLane;
                switch (noteArea)
                {
                    case SelectedNoteArea.Left:
                        {
                            var diff = prevSize.Size - lane;
                            if (1 <= diff && diff <= MadcaEnv.LaneCount)
                            {
                                note.ReLocate(new LanePotision(prevPos.Lane.RawLane + lane), note.Timing);
                            }
                            note.ReSize(new NoteSize(prevSize.Size - lane));
                            return;
                        }
                    case SelectedNoteArea.Right:
                        {
                            note.ReSize(new NoteSize(prevSize.Size + lane));
                            return;
                        }
                    default: return;
                }
            };

            box.MouseUp += (s, e) =>
            {
                if (!holder.UnLock(key)) { return; }
                var ops = new List<Operation>();
                // TODO: 条件式要検討
                if (prevPos.Lane.RawLane != note.Lane.RawLane || prevPos.Timing != note.Timing)
                {
                    ops.Add(new ReLocateNoteOperation(note, prevPos, new Position(note.Lane, note.Timing)));
                }
                if (prevSize != note.NoteSize)
                {
                    ops.Add(new ReSizeNoteOperation(note, prevSize, note.NoteSize));
                }
                if (ops.Any())
                {
                    opManager.AddOperation(new CompositeOperation(ops.ToArray()));
                }
                note = null;
            };
        }

        private static void SetEventToSetCursor(MadcaDisplay display, NoteBook noteBook, IReadOnlyEditorStatus status)
        {
            var box = display.PictureBox;
            var env = display.EditorLaneEnvironment;
            box.MouseMove += (s, e) =>
            {
                if (status.EditorMode != EditorMode.Edit)
                {
                    box.Cursor = Cursors.Default;
                    return;
                }
                if (!GetSelectedNote(e.Location, display, noteBook, out var note))
                {
                    box.Cursor = Cursors.Default;
                    return;
                }
                var area = note.SelectedNoteArea(e.Location, env);
                if (area == SelectedNoteArea.Left || area == SelectedNoteArea.Right)
                {
                    box.Cursor = Cursors.SizeWE;
                    return;
                }
                if (area == SelectedNoteArea.Center)
                {
                    box.Cursor = Cursors.SizeAll;
                    return;
                }
                box.Cursor = Cursors.Default;
            };
        }

        private static void SetEventToDeleteNote(MadcaDisplay display, NoteBook noteBook, OperationManager opManager, IReadOnlyEditorStatus status)
        {
            display.PictureBox.MouseDown += (s, e) =>
            {
                var env = display.EditorLaneEnvironment;
                if (status.EditorMode != EditorMode.Delete) { return; }
                var area = env.GetEditorLaneRegion(e.Location);
                if (area == EditorLaneRegion.Lane && e.Button == MouseButtons.Left)
                {
                    var note = noteBook.Notes.FindLast(x => x.GetRectangle(env).ContainsEx(e.Location, env));
                    if (note != null)
                    {
                        opManager.AddAndInvokeOperation(new DeleteShortNoteOperation(noteBook, note));
                        return;
                    }

                    foreach(var hold in noteBook.Holds.Reverse())
                    {
                        if (hold.HoldBegin.GetRectangle(env).ContainsEx(e.Location, env))
                        {
                            opManager.AddAndInvokeOperation(new DeleteHoldOperation(noteBook, hold));
                            return;
                        }
                        foreach(var step in hold.StepNotes)
                        {
                            if (step.GetRectangle(env).ContainsEx(e.Location, env))
                            {
                                opManager.AddAndInvokeOperation(new DeleteHoldStepOperation(hold, step));
                                return;
                            }
                        }
                    }
                }
            };
        }

        /// <summary>
        /// Refreshはここで一元管理する（ここ以外では呼び出さない）ことにする
        /// </summary>
        /// <param name="box">描画先のPictureBox</param>
        private static void SetEventForRefreshRule(MadcaDisplay display)
        {
            var box = display.PictureBox;
            box.MouseDown += (s, e) => box.Refresh();
            box.MouseMove += (s, e) => box.Refresh();
            box.SizeChanged += (s, e) => box.Refresh();
            box.MouseWheel += (s, e) => box.Refresh();
        }

        #endregion
    }

    public interface IReadOnlyEditorStatus
    {
        EditorMode EditorMode { get; }
        NoteMode NoteMode { get; }
        int NoteSize { get; }
        TimingPosition BeatStride { get; }
    }

    public class EditorStatus : IReadOnlyEditorStatus
    {
        public EditorMode EditorMode { get; set; }
        public NoteMode NoteMode { get; set; }
        public int NoteSize { get; set; }
        public TimingPosition BeatStride { get; set; }

        private EditorStatus() { }
        public EditorStatus(EditorMode edit, NoteMode note, int size, TimingPosition beat)
        {
            EditorMode = edit;
            NoteMode = note;
            NoteSize = size;
            BeatStride = beat;
        }
    }
}
