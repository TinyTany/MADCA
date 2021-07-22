using MADCA.Core.IO;
using MADCA.Core.Note;
using MADCA.Core.Score;
using System;
using JsonObject = System.Collections.Generic.Dictionary<string, dynamic>;

namespace MADCA.Core.FumenData
{
    public enum FumenDifficulity
    {
        Normal, Hard, Expert, Inferno
    }

    public class MadcaFumenData : IExchangeable
    {
        public ScoreBook ScoreBook { get; } = new ScoreBook();
        public NoteBook NoteBook { get; } = new NoteBook();
        public MadcaMusicData MadcaMusicData { get; } = new MadcaMusicData();
        public string FumenAuthor { get; private set; } = "Author";
        public FumenDifficulity FumenDifficulity { get; private set; } = FumenDifficulity.Normal;
        public double FumenConstant { get; private set; } = 1;
        public FumenLevel FumenLevel { get; } = new FumenLevel();
        public double StartBpm { get; private set; } = 120;

        public MadcaFumenData() { }
        public MadcaFumenData(
            ScoreBook scoreBook,
            NoteBook noteBook,
            MadcaMusicData madcaMusicData,
            string fumenAuthor,
            FumenDifficulity fumenDifficulity,
            double fumenConstant,
            FumenLevel fumenLevel,
            double startBpm)
        {
            ScoreBook = scoreBook;
            NoteBook = noteBook;
            MadcaMusicData = madcaMusicData;
            FumenAuthor = fumenAuthor;
            FumenDifficulity = fumenDifficulity;
            FumenConstant = fumenConstant;
            FumenLevel = fumenLevel;
            StartBpm = startBpm;
        }

        public JsonObject Exchange()
        {
            return new JsonObject()
            {
                ["ScoreBook"] = ScoreBook.Exchange(),
                ["NoteBook"] = NoteBook.Exchange(),
                ["MusicData"] = MadcaMusicData.Exchange(),
                ["FumenDesigner"] = FumenAuthor,
                ["FumenDifficulity"] = FumenDifficulity,
                ["FumenConstant"] = FumenConstant,
                ["FumenLevel"] = FumenLevel.Exchange(),
                ["StartBpm"] = StartBpm
            };
        }

        public void Exchange(JsonObject json)
        {
            ScoreBook.Exchange(json["ScoreBook"]);
            NoteBook.Exchange(json["NoteBook"]);
            MadcaMusicData.Exchange(json["MusicData"]);
            FumenAuthor = json["FumenDesigner"];
            FumenDifficulity = FumenData.FumenDifficulity.Parse(typeof(FumenDifficulity), json["FumenDifficulity"]);
            FumenConstant = double.Parse(json["FumenConstant"]);
            FumenLevel.Exchange(json["FumenLevel"]);
            StartBpm = double.Parse(json["StartBpm"]);
        }
    }

    public class MadcaMusicData : IExchangeable
    {
        public string MusicName { get; private set; } = "Music";
        public string MusicAuthor { get; private set; } = "Author";
        public double Bpm { get; private set; } = 120;
        public string MusicFilePath { get; private set; } = "";

        public MadcaMusicData() { }
        public MadcaMusicData(
            string musicName,
            string musicAuthor,
            double bpm,
            string musicFilePath)
        {
            MusicName = musicName;
            MusicAuthor = musicAuthor;
            Bpm = bpm;
            MusicFilePath = musicFilePath;
        }

        public JsonObject Exchange()
        {
            return new JsonObject()
            {
                ["MusicName"] = MusicName,
                ["MusicAuthor"] = MusicAuthor,
                ["Bpm"] = Bpm,
                ["MusicFilePath"] = MusicFilePath
            };
        }

        public void Exchange(JsonObject json)
        {
            MusicName = json["MusicName"];
            MusicAuthor = json["MusicAuthor"];
            Bpm = double.Parse(json["Bpm"]);
            MusicFilePath = json["MusicFilePath"];
        }
    }

    public class FumenLevel : IExchangeable
    {
        public int Level { get; private set; } = 1;
        public bool Plus { get; private set; } = false;
        public string FumenLevelString => $"{Level}{(Plus ? "+" : "")}";

        public FumenLevel() { }
        public FumenLevel(int level, bool plus)
        {
            System.Diagnostics.Debug.Assert(level > 0);
            Level = level;
            Plus = plus;
        }

        public JsonObject Exchange()
        {
            return new JsonObject()
            {
                ["Level"] = Level,
                ["Plus"] = Plus
            };
        }

        public void Exchange(JsonObject json)
        {
            Level = int.Parse(json["Level"]);
            Plus = bool.Parse(json["Plus"]);
        }
    }
}
