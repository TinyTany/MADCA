using MADCA.Core.Note;
using MADCA.Core.Score;

namespace MADCA.Core
{
    public sealed class MusicScore
    {
        private readonly ScoreBook scoreBook;
        private readonly NoteBook noteBook;

        public MusicScore()
        {
            scoreBook = new ScoreBook();
            noteBook = new NoteBook();
        }

        // TODO: NOTE: ノーツや小節の操作はScoreBook、NoteBookを触るのではなく、
        //             このクラスを介してやるようにしたいのでそういう実装をする
    }
}
