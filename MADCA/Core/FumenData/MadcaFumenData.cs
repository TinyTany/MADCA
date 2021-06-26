using MADCA.Core.Note;
using MADCA.Core.Score;

namespace MADCA.Core.FumenData
{
    enum FumenDifficulity
    {
        Normal, Hard, Expert, Inferno
    }

    class MadcaFumenData
    {
        public ScoreBook ScoreBook { get; } = new ScoreBook();
        public NoteBook NoteBook { get; } = new NoteBook();
        public MadcaMusicData MadcaMusicData { get; } = new MadcaMusicData();
        public string FumenAuthor { get; } = "Author";
        public FumenDifficulity FumenDifficulity { get; } = FumenDifficulity.Normal;
        public double FumenConstant { get; } = 1;
        public FumenLevel FumenLevel { get; } = new FumenLevel();
        public double StartBpm { get; } = 120;

        public MadcaFumenData() { }

        public MadcaFumenData(
            ScoreBook scores,
            NoteBook notes,
            MadcaMusicData musicData,
            string fumenAuthor,
            FumenDifficulity difficulity,
            double fumenConstant,
            FumenLevel level,
            double startBpm)
        {
            ScoreBook = scores;
            NoteBook = notes;
            MadcaMusicData = musicData;
            FumenAuthor = fumenAuthor;
            FumenDifficulity = difficulity;
            FumenConstant = fumenConstant;
            FumenLevel = level;
            StartBpm = startBpm;
        }
    }

    class MadcaMusicData
    {
        public string MusicName { get; } = "Music";
        public string MusicAuthor { get; } = "Author";
        public double Bpm { get; } = 120;
        public string MusicFilePath { get; } = "";

        public MadcaMusicData() { }

        public MadcaMusicData(
            string name,
            string author,
            double bpm,
            string path)
        {
            MusicName = name;
            MusicAuthor = author;
            Bpm = bpm;
            MusicFilePath = path;
        }
    }

    class FumenLevel
    {
        public int Level { get; } = 1;
        public bool Plus { get; } = false;
        public string FumenLevelString => $"{Level}{(Plus ? "+" : "")}";

        public FumenLevel() { }

        public FumenLevel(int level, bool plus)
        {
            System.Diagnostics.Debug.Assert(level > 0);
            Level = level;
            Plus = plus;
        }
    }
}
