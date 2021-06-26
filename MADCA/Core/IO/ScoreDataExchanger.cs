using MADCA.Core.FumenData;

namespace MADCA.Core.IO
{
    interface IMadcaIO
    {
        MadcaFumenData Import();
        void Export(MadcaFumenData fumen);
    }

    class ScoreDataExchanger
    {
    }
}
