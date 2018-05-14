using SocialNetworkConnection;
using System.Collections.Generic;

namespace Analytics
{
    public interface IAnalityc
    {

        IDictionary<string, int> Decided(IList<IPublication> publications);

        int[] Decided(int[][] input);
        int Decided(int[] input);
        int Decided(string[] input);
        int[] Decided(string path,int type);
        void ImportWordBank(string path);
        int[] ConvertTextInNumber(string[] msm);
        void MergeWordBanks(string path);
        void AddWordsToWordBank(string[] words,int[] ponde);
        void AddWordToWordBank(string word,int pon);
        int GetPond(string word);
        IDictionary<string, int> GetWordbank();
        void ExportWordBank(string path);
        void SaveToProcesNumber(string path);



    }
}
