namespace MADCA.UI
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbNew = new System.Windows.Forms.ToolStripButton();
            this.tsbOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbExport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbUndo = new System.Windows.Forms.ToolStripButton();
            this.tsbRedo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbEdit = new System.Windows.Forms.ToolStripButton();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tslBeat = new System.Windows.Forms.ToolStripLabel();
            this.tscbBeat = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tslNoteSize = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tsbTouch = new System.Windows.Forms.ToolStripButton();
            this.tsbChain = new System.Windows.Forms.ToolStripButton();
            this.tsbSlideL = new System.Windows.Forms.ToolStripButton();
            this.tsbSlideR = new System.Windows.Forms.ToolStripButton();
            this.tsbSnapU = new System.Windows.Forms.ToolStripButton();
            this.tsbSnapD = new System.Windows.Forms.ToolStripButton();
            this.tsbHold = new System.Windows.Forms.ToolStripButton();
            this.tsbHoldStep = new System.Windows.Forms.ToolStripButton();
            this.tsbField = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsbAddFavoriteSize = new System.Windows.Forms.ToolStripButton();
            this.tscbFavoriteNoteSize = new System.Windows.Forms.ToolStripComboBox();
            this.tsbDeleteFavoriteSize = new System.Windows.Forms.ToolStripButton();
            this.tsmiNew = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiCut = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.tsmiEdit});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1634, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // tsmiFile
            // 
            this.tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNew,
            this.tsmiOpen,
            this.tsmiSave,
            this.tsmiSaveAs,
            this.toolStripSeparator5,
            this.tsmiQuit});
            this.tsmiFile.Name = "tsmiFile";
            this.tsmiFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F)));
            this.tsmiFile.Size = new System.Drawing.Size(67, 20);
            this.tsmiFile.Text = "ファイル(&F)";
            // 
            // tsmiOpen
            // 
            this.tsmiOpen.Name = "tsmiOpen";
            this.tsmiOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.tsmiOpen.Size = new System.Drawing.Size(257, 22);
            this.tsmiOpen.Text = "開く(&O)...";
            // 
            // tsmiQuit
            // 
            this.tsmiQuit.Name = "tsmiQuit";
            this.tsmiQuit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.tsmiQuit.Size = new System.Drawing.Size(257, 22);
            this.tsmiQuit.Text = "終了(&Q)";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNew,
            this.tsbOpen,
            this.tsbSave,
            this.tsbExport,
            this.toolStripSeparator2,
            this.tsbUndo,
            this.tsbRedo,
            this.toolStripSeparator1,
            this.tsbAdd,
            this.tsbEdit,
            this.tsbDelete,
            this.toolStripSeparator3,
            this.tslBeat,
            this.tscbBeat,
            this.toolStripSeparator4,
            this.tslNoteSize,
            this.tsbAddFavoriteSize,
            this.tscbFavoriteNoteSize,
            this.tsbDeleteFavoriteSize});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1634, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbNew
            // 
            this.tsbNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbNew.Image = ((System.Drawing.Image)(resources.GetObject("tsbNew.Image")));
            this.tsbNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNew.Name = "tsbNew";
            this.tsbNew.Size = new System.Drawing.Size(35, 22);
            this.tsbNew.Text = "New";
            // 
            // tsbOpen
            // 
            this.tsbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbOpen.Image = ((System.Drawing.Image)(resources.GetObject("tsbOpen.Image")));
            this.tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpen.Name = "tsbOpen";
            this.tsbOpen.Size = new System.Drawing.Size(40, 22);
            this.tsbOpen.Text = "Open";
            // 
            // tsbSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(35, 22);
            this.tsbSave.Text = "Save";
            // 
            // tsbExport
            // 
            this.tsbExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbExport.Image = ((System.Drawing.Image)(resources.GetObject("tsbExport.Image")));
            this.tsbExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExport.Name = "tsbExport";
            this.tsbExport.Size = new System.Drawing.Size(45, 22);
            this.tsbExport.Text = "Export";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbUndo
            // 
            this.tsbUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbUndo.Image = ((System.Drawing.Image)(resources.GetObject("tsbUndo.Image")));
            this.tsbUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUndo.Name = "tsbUndo";
            this.tsbUndo.Size = new System.Drawing.Size(40, 22);
            this.tsbUndo.Text = "Undo";
            // 
            // tsbRedo
            // 
            this.tsbRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbRedo.Image = ((System.Drawing.Image)(resources.GetObject("tsbRedo.Image")));
            this.tsbRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRedo.Name = "tsbRedo";
            this.tsbRedo.Size = new System.Drawing.Size(38, 22);
            this.tsbRedo.Text = "Redo";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbAdd
            // 
            this.tsbAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbAdd.Image = ((System.Drawing.Image)(resources.GetObject("tsbAdd.Image")));
            this.tsbAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAdd.Name = "tsbAdd";
            this.tsbAdd.Size = new System.Drawing.Size(33, 22);
            this.tsbAdd.Text = "Add";
            // 
            // tsbEdit
            // 
            this.tsbEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbEdit.Image = ((System.Drawing.Image)(resources.GetObject("tsbEdit.Image")));
            this.tsbEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEdit.Name = "tsbEdit";
            this.tsbEdit.Size = new System.Drawing.Size(31, 22);
            this.tsbEdit.Text = "Edit";
            // 
            // tsbDelete
            // 
            this.tsbDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsbDelete.Image")));
            this.tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDelete.Name = "tsbDelete";
            this.tsbDelete.Size = new System.Drawing.Size(44, 22);
            this.tsbDelete.Text = "Delete";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tslBeat
            // 
            this.tslBeat.Name = "tslBeat";
            this.tslBeat.Size = new System.Drawing.Size(30, 22);
            this.tslBeat.Text = "Beat";
            // 
            // tscbBeat
            // 
            this.tscbBeat.AutoSize = false;
            this.tscbBeat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbBeat.DropDownWidth = 60;
            this.tscbBeat.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.tscbBeat.Name = "tscbBeat";
            this.tscbBeat.Size = new System.Drawing.Size(60, 23);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tslNoteSize
            // 
            this.tslNoteSize.Name = "tslNoteSize";
            this.tslNoteSize.Size = new System.Drawing.Size(53, 22);
            this.tslNoteSize.Text = "NoteSize";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbTouch,
            this.tsbChain,
            this.tsbSlideL,
            this.tsbSlideR,
            this.tsbSnapU,
            this.tsbSnapD,
            this.tsbHold,
            this.tsbHoldStep,
            this.tsbField,
            this.toolStripLabel1});
            this.toolStrip2.Location = new System.Drawing.Point(0, 49);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(1634, 25);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // tsbTouch
            // 
            this.tsbTouch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbTouch.Image = ((System.Drawing.Image)(resources.GetObject("tsbTouch.Image")));
            this.tsbTouch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTouch.Name = "tsbTouch";
            this.tsbTouch.Size = new System.Drawing.Size(43, 22);
            this.tsbTouch.Text = "Touch";
            // 
            // tsbChain
            // 
            this.tsbChain.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbChain.Image = ((System.Drawing.Image)(resources.GetObject("tsbChain.Image")));
            this.tsbChain.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbChain.Name = "tsbChain";
            this.tsbChain.Size = new System.Drawing.Size(41, 22);
            this.tsbChain.Text = "Chain";
            // 
            // tsbSlideL
            // 
            this.tsbSlideL.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbSlideL.Image = ((System.Drawing.Image)(resources.GetObject("tsbSlideL.Image")));
            this.tsbSlideL.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSlideL.Name = "tsbSlideL";
            this.tsbSlideL.Size = new System.Drawing.Size(42, 22);
            this.tsbSlideL.Text = "SlideL";
            // 
            // tsbSlideR
            // 
            this.tsbSlideR.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbSlideR.Image = ((System.Drawing.Image)(resources.GetObject("tsbSlideR.Image")));
            this.tsbSlideR.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSlideR.Name = "tsbSlideR";
            this.tsbSlideR.Size = new System.Drawing.Size(43, 22);
            this.tsbSlideR.Text = "SlideR";
            // 
            // tsbSnapU
            // 
            this.tsbSnapU.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbSnapU.Image = ((System.Drawing.Image)(resources.GetObject("tsbSnapU.Image")));
            this.tsbSnapU.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSnapU.Name = "tsbSnapU";
            this.tsbSnapU.Size = new System.Drawing.Size(45, 22);
            this.tsbSnapU.Text = "SnapU";
            // 
            // tsbSnapD
            // 
            this.tsbSnapD.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbSnapD.Image = ((System.Drawing.Image)(resources.GetObject("tsbSnapD.Image")));
            this.tsbSnapD.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSnapD.Name = "tsbSnapD";
            this.tsbSnapD.Size = new System.Drawing.Size(45, 22);
            this.tsbSnapD.Text = "SnapD";
            // 
            // tsbHold
            // 
            this.tsbHold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbHold.Image = ((System.Drawing.Image)(resources.GetObject("tsbHold.Image")));
            this.tsbHold.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHold.Name = "tsbHold";
            this.tsbHold.Size = new System.Drawing.Size(37, 22);
            this.tsbHold.Text = "Hold";
            // 
            // tsbHoldStep
            // 
            this.tsbHoldStep.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbHoldStep.Image = ((System.Drawing.Image)(resources.GetObject("tsbHoldStep.Image")));
            this.tsbHoldStep.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHoldStep.Name = "tsbHoldStep";
            this.tsbHoldStep.Size = new System.Drawing.Size(60, 22);
            this.tsbHoldStep.Text = "HoldStep";
            // 
            // tsbField
            // 
            this.tsbField.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbField.Image = ((System.Drawing.Image)(resources.GetObject("tsbField.Image")));
            this.tsbField.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbField.Name = "tsbField";
            this.tsbField.Size = new System.Drawing.Size(36, 22);
            this.tsbField.Text = "Field";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(86, 22);
            this.toolStripLabel1.Text = "toolStripLabel1";
            // 
            // tsbAddFavoriteSize
            // 
            this.tsbAddFavoriteSize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbAddFavoriteSize.Image = ((System.Drawing.Image)(resources.GetObject("tsbAddFavoriteSize.Image")));
            this.tsbAddFavoriteSize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddFavoriteSize.Name = "tsbAddFavoriteSize";
            this.tsbAddFavoriteSize.Size = new System.Drawing.Size(23, 22);
            this.tsbAddFavoriteSize.Text = "→";
            this.tsbAddFavoriteSize.ToolTipText = "選択中のサイズをお気に入りに追加します";
            // 
            // tscbFavoriteNoteSize
            // 
            this.tscbFavoriteNoteSize.AutoSize = false;
            this.tscbFavoriteNoteSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbFavoriteNoteSize.DropDownWidth = 60;
            this.tscbFavoriteNoteSize.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.tscbFavoriteNoteSize.Name = "tscbFavoriteNoteSize";
            this.tscbFavoriteNoteSize.Size = new System.Drawing.Size(60, 23);
            // 
            // tsbDeleteFavoriteSize
            // 
            this.tsbDeleteFavoriteSize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbDeleteFavoriteSize.Image = ((System.Drawing.Image)(resources.GetObject("tsbDeleteFavoriteSize.Image")));
            this.tsbDeleteFavoriteSize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDeleteFavoriteSize.Name = "tsbDeleteFavoriteSize";
            this.tsbDeleteFavoriteSize.Size = new System.Drawing.Size(35, 22);
            this.tsbDeleteFavoriteSize.Text = "削除";
            this.tsbDeleteFavoriteSize.ToolTipText = "選択中のサイズをお気に入りから削除します";
            // 
            // tsmiNew
            // 
            this.tsmiNew.Name = "tsmiNew";
            this.tsmiNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.tsmiNew.Size = new System.Drawing.Size(257, 22);
            this.tsmiNew.Text = "新規作成(&N)";
            // 
            // tsmiSave
            // 
            this.tsmiSave.Name = "tsmiSave";
            this.tsmiSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.tsmiSave.Size = new System.Drawing.Size(257, 22);
            this.tsmiSave.Text = "上書き保存(&S)";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(254, 6);
            // 
            // tsmiSaveAs
            // 
            this.tsmiSaveAs.Name = "tsmiSaveAs";
            this.tsmiSaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.tsmiSaveAs.Size = new System.Drawing.Size(257, 22);
            this.tsmiSaveAs.Text = "名前を付けて保存(&A)...";
            // 
            // tsmiEdit
            // 
            this.tsmiEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiUndo,
            this.tsmiRedo,
            this.toolStripSeparator6,
            this.tsmiCut,
            this.tsmiCopy,
            this.tsmiPaste,
            this.tsmiDelete});
            this.tsmiEdit.Name = "tsmiEdit";
            this.tsmiEdit.Size = new System.Drawing.Size(57, 20);
            this.tsmiEdit.Text = "編集(&E)";
            // 
            // tsmiUndo
            // 
            this.tsmiUndo.Name = "tsmiUndo";
            this.tsmiUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.tsmiUndo.Size = new System.Drawing.Size(180, 22);
            this.tsmiUndo.Text = "元に戻す";
            // 
            // tsmiRedo
            // 
            this.tsmiRedo.Name = "tsmiRedo";
            this.tsmiRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.tsmiRedo.Size = new System.Drawing.Size(180, 22);
            this.tsmiRedo.Text = "やり直し";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(177, 6);
            // 
            // tsmiCut
            // 
            this.tsmiCut.Name = "tsmiCut";
            this.tsmiCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.tsmiCut.Size = new System.Drawing.Size(180, 22);
            this.tsmiCut.Text = "切り取り";
            // 
            // tsmiCopy
            // 
            this.tsmiCopy.Name = "tsmiCopy";
            this.tsmiCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.tsmiCopy.Size = new System.Drawing.Size(180, 22);
            this.tsmiCopy.Text = "コピー";
            // 
            // tsmiPaste
            // 
            this.tsmiPaste.Name = "tsmiPaste";
            this.tsmiPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.tsmiPaste.Size = new System.Drawing.Size(180, 22);
            this.tsmiPaste.Text = "貼り付け";
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.tsmiDelete.Size = new System.Drawing.Size(180, 22);
            this.tsmiDelete.Text = "削除";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1634, 861);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem tsmiFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiQuit;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpen;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbAdd;
        private System.Windows.Forms.ToolStripButton tsbEdit;
        private System.Windows.Forms.ToolStripButton tsbDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbUndo;
        private System.Windows.Forms.ToolStripButton tsbRedo;
        private System.Windows.Forms.ToolStripButton tsbNew;
        private System.Windows.Forms.ToolStripButton tsbOpen;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripButton tsbExport;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton tsbField;
        private System.Windows.Forms.ToolStripButton tsbTouch;
        private System.Windows.Forms.ToolStripButton tsbChain;
        private System.Windows.Forms.ToolStripButton tsbSlideL;
        private System.Windows.Forms.ToolStripButton tsbSlideR;
        private System.Windows.Forms.ToolStripButton tsbSnapU;
        private System.Windows.Forms.ToolStripButton tsbSnapD;
        private System.Windows.Forms.ToolStripButton tsbHold;
        private System.Windows.Forms.ToolStripButton tsbHoldStep;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripComboBox tscbBeat;
        private System.Windows.Forms.ToolStripLabel tslBeat;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel tslNoteSize;
        private System.Windows.Forms.ToolStripButton tsbAddFavoriteSize;
        private System.Windows.Forms.ToolStripComboBox tscbFavoriteNoteSize;
        private System.Windows.Forms.ToolStripButton tsbDeleteFavoriteSize;
        private System.Windows.Forms.ToolStripMenuItem tsmiNew;
        private System.Windows.Forms.ToolStripMenuItem tsmiSave;
        private System.Windows.Forms.ToolStripMenuItem tsmiSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem tsmiEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmiUndo;
        private System.Windows.Forms.ToolStripMenuItem tsmiRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem tsmiCut;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopy;
        private System.Windows.Forms.ToolStripMenuItem tsmiPaste;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
    }
}