using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MGCTemplate.ViewModels.Clues
{
    public class ReverseAlphabetViewModel:mvvmHandlers
    {
        #region "Properties"

        private System.Collections.ObjectModel.ObservableCollection<Pocos.ReverseAlphaPOCO> reverseAlphaLstMaj = new System.Collections.ObjectModel.ObservableCollection<Pocos.ReverseAlphaPOCO>();
        public System.Collections.ObjectModel.ObservableCollection<Pocos.ReverseAlphaPOCO> ReverseAlphaLstMaj
        {
            get
            {
                return reverseAlphaLstMaj;
            }
            set
            {

                reverseAlphaLstMaj = value;
                NotifyPropertyChanged("ReverseAlphaLstMaj");
            }
        }

        private System.Collections.ObjectModel.ObservableCollection<Pocos.ReverseAlphaPOCO> reverseAlphaLstMin = new System.Collections.ObjectModel.ObservableCollection<Pocos.ReverseAlphaPOCO>();
        public System.Collections.ObjectModel.ObservableCollection<Pocos.ReverseAlphaPOCO> ReverseAlphaLstMin
        {
            get
            {
                return reverseAlphaLstMin;
            }
            set
            {

                reverseAlphaLstMin = value;
                NotifyPropertyChanged("ReverseAlphaLstMin");
            }
        }

        private Pocos.ReverseAlphaPOCO currentReverseAlpha = new Pocos.ReverseAlphaPOCO();
        public Pocos.ReverseAlphaPOCO CurrentReverseAlpha
        {
            get
            {
                return currentReverseAlpha;
            }
            set
            {

                currentReverseAlpha = value;
                if (currentReverseAlpha != null)
                {
                    currentReverseAlpha.FontWeight = "Bold";
                }
                NotifyPropertyChanged("CurrentReverseAlpha");
            }
        }

        private String clue = String.Empty;
        public String Clue
        {
            get
            {
                return clue;
            }
            set
            {

                clue = value;
                HiddenClue = GetAssociatedLetter(value);
                NotifyPropertyChanged("Clue");

            }
        }

        private String hiddenClue = String.Empty;
        public String HiddenClue
        {
            get
            {
                return hiddenClue;
            }
            set
            {

                hiddenClue = value;
                NotifyPropertyChanged("HiddenClue");
            }
        }

        #endregion //Properties
        public ReverseAlphabetViewModel()
        {
            CreateReverseAlpha();
        }

        #region "Functions"

        private String GetAssociatedLetter(String clue)
        {
            string hiddenClue = String.Empty;
            foreach (char c in clue)
            {
                hiddenClue += GetRelatedCharacter(c);
            }

            return hiddenClue;
        }

        private char GetRelatedCharacter(char c)
        {

            if (ReverseAlphaLstMaj.Any(i => i.FirstLetter == c))
            {
                CurrentReverseAlpha = null;
                CurrentReverseAlpha = ReverseAlphaLstMaj.Where(x => x.FirstLetter == c).FirstOrDefault();
                char inverseChar = CurrentReverseAlpha.SecondLetter;
                //char inverseChar = ReverseAlphaLstMaj.Where(x => x.FirstLetter == c).FirstOrDefault().SecondLetter;

                return inverseChar;
            }
            else if (ReverseAlphaLstMin.Any(i => i.FirstLetter == c))
            {
                CurrentReverseAlpha = null;
                CurrentReverseAlpha = ReverseAlphaLstMin.Where(x => x.FirstLetter == c).FirstOrDefault();
                //CurrentReverseAlpha.FontWeight = "Bold";
                //char inverseChar = ReverseAlphaLstMin.Where(x => x.FirstLetter == c).FirstOrDefault().SecondLetter;
                char inverseChar = CurrentReverseAlpha.SecondLetter;

                return inverseChar;
            }
            else
            {
                CurrentReverseAlpha = null;
                return c;
            }
        }

        #endregion //Functions

        #region "Commands"

        private ICommand createReverseAlphaCommand;
        public ICommand CreateReverseAlphaCommand
        {
            get
            {
                return createReverseAlphaCommand ?? (createReverseAlphaCommand = new CommandHandler(() => CreateReverseAlpha(), _canExecute));
            }
        }
        private void CreateReverseAlpha()
        {
            //ReverseAlphaLst = new System.Collections.ObjectModel.ObservableCollection<Classes.ReverseAlpha>();
            int id = 0;

            char assciCntMaj = 'Z';
            for (char c = 'A'; c <= 'Z'; c++)
            {
                Pocos.ReverseAlphaPOCO insert = new Pocos.ReverseAlphaPOCO { Id = id, FirstLetter = c, SecondLetter = assciCntMaj, FontWeight = "Normal" };
                assciCntMaj--;
                id++;
                ReverseAlphaLstMaj.Add(insert);
            }
            char assciCntMin = 'z';
            for (char c = 'a'; c <= 'z'; c++)
            {
                Pocos.ReverseAlphaPOCO insert = new Pocos.ReverseAlphaPOCO { Id = id, FirstLetter = c, SecondLetter = assciCntMin, FontWeight = "Normal" };
                assciCntMin--;
                id++;
                ReverseAlphaLstMin.Add(insert);
            }

            int stop = 0;

        }

        #endregion //Commands
    }
}
