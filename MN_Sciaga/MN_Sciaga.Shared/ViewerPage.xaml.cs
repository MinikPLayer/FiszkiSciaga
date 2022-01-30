using MN_Sciaga.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MN_Sciaga
{
    public class QuestionEntryModel
    {
        public string header;
        public List<TextBlock> content = new List<TextBlock>();
    }

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ViewerPage : Page
    {
        ObservableObject<QuestionEntryModel> selectedQuestion = new ObservableObject<QuestionEntryModel>();
        int curPageID = 0;

        public List<QuestionEntryModel> questions = new List<QuestionEntryModel>();

        const int fontSize = 32;

        public void ParseFile(string[] lines)
        {
            QuestionEntryModel model = null;
            for(int i = 0;i< lines.Length;i++)
            {
                var trimmed = lines[i].TrimStart('\t', ' ');
                // Header
                if(trimmed.StartsWith("*") || trimmed.StartsWith("•"))
                {
                    model = new QuestionEntryModel();
                    model.header = trimmed;
                    questions.Add(model);
                    continue;
                }
                if (model == null)
                    continue;

                TextBlock block = new TextBlock();
                block.Text = lines[i];
                block.FontSize = fontSize;
                if (trimmed.StartsWith("+"))
                    block.FontWeight = Windows.UI.Text.FontWeights.Bold;
                if (trimmed.StartsWith("-"))
                    block.Text = block.Text.Insert(0, "\t");

                model.content.Add(block);
            }
        }

        public ViewerPage()
        {
            this.InitializeComponent();
        }

        void NextPage(object sender, RoutedEventArgs e) => SelectElement(++curPageID);
        void PrevPage(object sender, RoutedEventArgs e) => SelectElement(--curPageID);

        void SelectElement(int id)
        {
            if (id < 0)
                id = 0;
            if (id >= questions.Count)
                id = questions.Count - 1;

            var quest = questions[id];
            selectedQuestion.val = quest;
            curPageID = id;

            questionContentSP.Children.Clear();
            for(int i = 0;i<quest.content.Count;i++)
            {
                questionContentSP.Children.Add(quest.content[i]);
            }

            pageNumberText.Text = (id + 1).ToString() + " / " + questions.Count.ToString();          
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            string[] lines = e.Parameter as string[];
            if(lines == null)
            {
                await new MessageDialog("Error converting page parameter or parameter not set", "Error").ShowAsync();
                return;
            }

            ParseFile(lines);

            SelectElement(0);
        }
    }
}
