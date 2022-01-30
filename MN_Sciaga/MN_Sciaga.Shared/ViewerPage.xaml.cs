using MN_Sciaga.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
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
    public static class StringExtensions
    {
        public static string FirstCharToUpper(this string input)
        {
            StringBuilder sb = new StringBuilder(input);
            if (sb[0] >= 'a' && sb[0] <= 'z')
                sb[0] -= (char)('a' - 'A');

            return sb.ToString();
        }
    }

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

        public ObservableObject<Brush> separatorColor = new ObservableObject<Brush>();

        public List<QuestionEntryModel> questions = new List<QuestionEntryModel>();

        const int fontSize = 32;

        public void ParseFile(string[] lines)
        {
            QuestionEntryModel model = null;
            for(int i = 0;i< lines.Length;i++)
            {
                var line = lines[i];
                var trimmed = line.TrimStart('\t', ' ');

                // Header
                if(trimmed.StartsWith("*") || trimmed.StartsWith("•"))
                {
                    model = new QuestionEntryModel();
                    model.header = trimmed.Remove(0, 2).TrimStart(' ').FirstCharToUpper(); // Remove * and space
                    questions.Add(model);
                    continue;
                }
                if (model == null)
                    continue;

                TextBlock block = new TextBlock();
                //block.Text = lines[i];

                if (trimmed.StartsWith("+"))
                    block.FontWeight = Windows.UI.Text.FontWeights.ExtraBold;
                else if (trimmed.StartsWith("-"))
                    line = "\t" + line;
                else
                    line = "\t\t" + line;

                int index = 0;
                while(true)
                {
                    string match = "**";

                    var pos = line.IndexOf(match, index);
                    if (pos == -1)
                        break;

                    var pos2 = line.IndexOf(match, pos + match.Length);
                    if (pos2 == -1)
                        break;

                    if (index != pos)
                    {
                        var text = line.Substring(index, pos - index);
                        block.Inlines.Add(new Run() { Text = text });
                    }

                    var iText = line.Substring(pos, pos2 - pos + match.Length);
                    // remove match
                    iText = iText.Remove(0, match.Length);
                    iText = iText.Substring(0, iText.Length - match.Length);

                    var inline = new Run() { Text = iText, FontWeight = Windows.UI.Text.FontWeights.Bold };
                    block.Inlines.Add(inline);

                    index = pos2 + match.Length;
                }

                if (index != line.Length)
                    block.Inlines.Add(new Run() { Text = line.Substring(index) });

                block.FontSize = fontSize;


                model.content.Add(block);
            }
        }

        public ViewerPage()
        {
            this.InitializeComponent();

            var uiSettings = new Windows.UI.ViewManagement.UISettings();
            separatorColor.val = new SolidColorBrush(uiSettings.GetColorValue(Windows.UI.ViewManagement.UIColorType.Accent));

            Window.Current.CoreWindow.KeyDown += PageKeyDown_Event;
        }

        ~ViewerPage()
        {
            Window.Current.CoreWindow.KeyDown -= PageKeyDown_Event;
        }

        void NextPage(object sender, RoutedEventArgs e) => SelectElement(curPageID + 1);
        void PrevPage(object sender, RoutedEventArgs e) => SelectElement(curPageID - 1);

        void PageKeyDown_Event(object sender, Windows.UI.Core.KeyEventArgs e)
        {
            if (e.VirtualKey == Windows.System.VirtualKey.Right)
                NextPage(null, null);
            else if (e.VirtualKey == Windows.System.VirtualKey.Left)
                PrevPage(null, null);
            

            e.Handled = true;
        }

        bool SelectElement(int id)
        {
            if (id < 0)
                return false;
            if (id >= questions.Count)
                return false;

            var quest = questions[id];
            selectedQuestion.val = quest;
            curPageID = id;

            questionContentSP.Children.Clear();
            for(int i = 0;i<quest.content.Count;i++)
            {
                questionContentSP.Children.Add(quest.content[i]);
            }

            //pageNumberText.Text = (id + 1).ToString() + " / " + questions.Count.ToString();
            pageNumberBox.Text = (id + 1).ToString();
            pagesCountText.Text = questions.Count.ToString();

            return true;
        }

        async void PageNumberChanged(object sender, KeyRoutedEventArgs e)
        {
            if(e.Key == Windows.System.VirtualKey.Enter)
            {
                TextBox box = (TextBox)sender;
                
                if(!int.TryParse(box.Text, out int index))
                {
                    await new MessageDialog("Error parsing page number - unknown character", "Error").ShowAsync();
                    return;
                }

                if(!SelectElement(index - 1))
                {
                    await new MessageDialog("Error setting page number - wrong page number", "Error").ShowAsync();
                    return;
                }
            }    

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
