using MADCA.Core.FumenData;
using MADCA.Core.Note;
using MADCA.Core.Score;
using System.Linq;
using JsonObject = System.Collections.Generic.Dictionary<string, dynamic>;

namespace MADCA.Core.IO
{
    public interface IExchangeable<T>
    {
        T Exchange();
        void Exchange(T t);
    }

    public abstract class Exchanger<T, U>
    {
        public abstract T Exchange(U u);
        public abstract U Exchange(T t);
    }

    public class FumenExchanger : Exchanger<MadcaFumenData, JsonObject>
    {
        public static FumenExchanger Instance { get; } = new FumenExchanger();

        private FumenExchanger() { }

        public override JsonObject Exchange(MadcaFumenData fumen)
        {
            var json = new JsonObject();
            json["ScoreBook"] = ScoreBookExchanger.Instance.Exchange(fumen.ScoreBook);
            json["NoteBook"] = NoteBookExchanger.Instance.Exchange(fumen.NoteBook);
            return null;
        }

        public override MadcaFumenData Exchange(JsonObject json)
        {
            return null;
        }
    }

    public class ScoreBookExchanger : Exchanger<ScoreBook, JsonObject>
    {
        public static ScoreBookExchanger Instance { get; } = new ScoreBookExchanger();

        private ScoreBookExchanger() { }

        public override ScoreBook Exchange(JsonObject json)
        {
            throw new System.NotImplementedException();
        }

        public override JsonObject Exchange(ScoreBook scores)
        {
            var json = new JsonObject();
            throw new System.NotImplementedException();
        }
    }

    public class NoteBookExchanger : Exchanger<NoteBook, JsonObject>
    {
        public static NoteBookExchanger Instance { get; } = new NoteBookExchanger();

        private NoteBookExchanger() { }

        public override NoteBook Exchange(JsonObject json)
        {
            throw new System.NotImplementedException();
        }

        public override JsonObject Exchange(NoteBook notes)
        {
            throw new System.NotImplementedException();
        }
    }
}
