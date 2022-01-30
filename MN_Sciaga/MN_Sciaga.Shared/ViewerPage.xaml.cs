using MN_Sciaga.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
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

        public static bool IsDashedNumber(this string input)
        {
            if (input.Length < 2)
                return false;

            for(int i = 0;i<input.Length;i++)
            {
                if(input[i] == ')')
                {
                    if (i == 0)
                        return false;

                    return true;
                }

                if (input[i] < '0' || input[i] > '9')
                    return false;
            }

            return false;
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
        public ObservableCollection<string> searchBarHints = new ObservableCollection<string>();

        const int fontSize = 32;

        public void ParseFile(string[] lines)
        {
            QuestionEntryModel model = null;
            for(int i = 0;i< lines.Length;i++)
            {
                var line = lines[i];
                var trimmed = line.TrimStart('\t', ' ');
                line = trimmed;

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

                const int tabSize = 40;
                if (trimmed.StartsWith("+"))
                {
                    block.FontWeight = Windows.UI.Text.FontWeights.ExtraBold;
                    var pos = line.IndexOf('+');
                    line = "\n" + line.Remove(pos, 1).Insert(pos, "•"); // replace + with • for better look
                }
                //else if (trimmed.StartsWith("-"))
                //    //line = "\t" + line;
                //    block.Margin = new Thickness(25, 0, 0, 0);
                //else
                //    //line = "\t\t" + line;
                //    block.Margin = new Thickness(50, 0, 0, 0);
                else if (trimmed.StartsWith("-"))
                {
                    int count = 0;
                    for (int j = 0; j < trimmed.Length; j++)
                    {
                        if (trimmed[j] == '-')
                            count++;
                        else
                            break;
                    }

                    line = line.Substring(count - 1);

                    block.Margin = new Thickness(tabSize * count, 0, 0, 0);
                }
                else if (trimmed.IsDashedNumber())
                    block.Margin = new Thickness(tabSize * 2, 0, 0, 0);
                else
                    block.Margin = new Thickness(tabSize * 3, 0, 0, 0);

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
                block.TextWrapping = TextWrapping.WrapWholeWords;


                model.content.Add(block);
            }
        }

        public ViewerPage()
        {
            this.InitializeComponent();

            var uiSettings = new Windows.UI.ViewManagement.UISettings();
            separatorColor.val = new SolidColorBrush(uiSettings.GetColorValue(Windows.UI.ViewManagement.UIColorType.Accent));

#if !__WASM__
            Window.Current.CoreWindow.KeyDown += PageKeyDown_Event;
#endif

        }

#if !__WASM__
        ~ViewerPage()
        {
            Window.Current.CoreWindow.KeyDown -= PageKeyDown_Event;
        }
#endif

        void SearchBoxKeyDown(object sender, KeyRoutedEventArgs e)
        {
            if(e.Key == Windows.System.VirtualKey.Enter)
            {
                if (searchBarHints.Count == 0)
                    return;

                if (hintsListBox.SelectedIndex == -1)
                    hintsListBox.SelectedIndex = 0;

                var item = (string)hintsListBox.SelectedItem;
                SelectElement(item);
                searchBar.Text = "";

                searchBarVisible.val = Visibility.Collapsed;
            }
            else if(e.Key == Windows.System.VirtualKey.Up)
            {
                if (hintsListBox.SelectedIndex == -1)
                    hintsListBox.SelectedIndex = 0;
                else if (hintsListBox.SelectedIndex > 0)
                    hintsListBox.SelectedIndex = hintsListBox.SelectedIndex - 1;
            }
            else if (e.Key == Windows.System.VirtualKey.Down)
            {
                if (hintsListBox.SelectedIndex == -1)
                    hintsListBox.SelectedIndex = 0;
                else if (hintsListBox.SelectedIndex < searchBarHints.Count - 1)
                    hintsListBox.SelectedIndex = hintsListBox.SelectedIndex + 1;
            }
        }

        void SearchTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox box = sender as TextBox;
            if(box == null)
            {
                _ = new MessageDialog("Internal app error - 231457a", "Error").ShowAsync();
                return;
            }

            searchBarHints.Clear();

            string text = box.Text;
            if (text == null || text.Length == 0)
                return;

            for(int i = 0;i<questions.Count;i++)
            {
                if (questions[i].header.ToLower().Contains(text.ToLower()))
                    searchBarHints.Add(questions[i].header);
            }
        }

        void HintsListBox_DT(object sender, TappedRoutedEventArgs e)
        {
            SelectElement((string)hintsListBox.SelectedItem);
            searchBar.Text = "";
            searchBarVisible.val = Visibility.Collapsed;
        }


        ObservableObject<Visibility> searchBarVisible = new ObservableObject<Visibility>(Visibility.Collapsed);
        void SearchButtonClicked(object sender, RoutedEventArgs e)
        {
            searchBarVisible.val = searchBarVisible.val == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        void NextPage(object sender, RoutedEventArgs e) => SelectElement(curPageID + 1, showDefault);
        void PrevPage(object sender, RoutedEventArgs e) => SelectElement(curPageID - 1, showDefault);


        List<int> randomList = new List<int>();
        int GetNewRandom()
        {
            if (questions.Count == 0)
                return 0;

            if(randomList.Count == 0)
            {
                for (int i = 0; i < questions.Count; i++)
                    randomList.Add(i);
            }

            var random = new Random().Next(0, randomList.Count);
            var value = randomList[random];
            randomList.RemoveAt(random);

            return value;
        }

        bool showDefault = true;
        void KeyPressed(Windows.System.VirtualKey key)
        {

            const double scrollAmount = 80;

            if (key == Windows.System.VirtualKey.Right)
                NextPage(null, null);
            else if (key == Windows.System.VirtualKey.Left)
                PrevPage(null, null);
            else if(key == Windows.System.VirtualKey.Up)
            {
                if (searchBarVisible.val == Visibility.Collapsed)
                    questionContentSV.ChangeView(questionContentSV.HorizontalOffset, questionContentSV.VerticalOffset - scrollAmount, 1);
            }
            else if(key == Windows.System.VirtualKey.Down)
            {
                if (searchBarVisible.val == Visibility.Collapsed)
                    questionContentSV.ChangeView(questionContentSV.HorizontalOffset, questionContentSV.VerticalOffset + scrollAmount, 1);
            }
            else if (key == Windows.System.VirtualKey.PageUp)
            {
                questionContentSV.ChangeView(questionContentSV.HorizontalOffset, questionContentSV.VerticalOffset - scrollAmount * 5, 1);
            }
            else if (key == Windows.System.VirtualKey.PageDown)
            {
                questionContentSV.ChangeView(questionContentSV.HorizontalOffset, questionContentSV.VerticalOffset + scrollAmount * 5, 1);
            }
            // Random
            else if(key == Windows.System.VirtualKey.R)
            {
                if(searchBarVisible.val == Visibility.Collapsed)
                {
                    var random = GetNewRandom();
                    SelectElement(random, showDefault);
                }
            }
            else if(key == Windows.System.VirtualKey.S)
            {
                if (searchBarVisible.val == Visibility.Collapsed)
                {
                    showDefault = true;
                    SelectElement(curPageID, true);
                }
            }
            else if (key == Windows.System.VirtualKey.H)
            {
                if (searchBarVisible.val == Visibility.Collapsed)
                {
                    showDefault = false;
                    SelectElement(curPageID, false);
                }
            }
            else if(key == Windows.System.VirtualKey.F12)
            {
                var view = ApplicationView.GetForCurrentView();
                if (view.IsFullScreenMode)
                {
                    view.ExitFullScreenMode();
                }
                else
                {
                    view.TryEnterFullScreenMode();
                }
            }
            else
            {
                if(key == Windows.System.VirtualKey.Control)
                {
                    if(searchBarVisible.val == Visibility.Collapsed)
                    {
                        searchBarVisible.val = Visibility.Visible;
                        searchBar.Focus(FocusState.Keyboard);
                    }
                    else if(searchBar.Text.Length == 0)
                    {
                        searchBarVisible.val = Visibility.Collapsed;
                    }

                }
            }
        }

        void PKA_Button(object sender, ProcessKeyboardAcceleratorEventArgs e)
        {
            KeyPressed(e.Key);

            e.Handled = true;
        }

        void ElementPageDown_Event(object sender, KeyRoutedEventArgs e)
        {
            KeyPressed(e.Key);

            e.Handled = true;
        }

        void PageKeyDown_Event(object sender, Windows.UI.Core.KeyEventArgs e)
        {
            KeyPressed(e.VirtualKey);

            e.Handled = true;
        }

        bool SelectElement(string name)
        {
            for (int i = 0; i < questions.Count; i++)
            {
                if (questions[i].header == name)
                {
                    return SelectElement(i);
                }
            }

            return false;
        }

        bool SelectElement(int id, bool revealContent = true)
        {
            if (id < 0)
                return false;
            if (id >= questions.Count)
                return false;

            var quest = questions[id];
            selectedQuestion.val = quest;
            curPageID = id;

            questionContentSP.Children.Clear();
            if(revealContent)
                for(int i = 0;i<quest.content.Count;i++)
                    questionContentSP.Children.Add(quest.content[i]);

            pageNumberBox.Text = (id + 1).ToString();
            pagesCountText.Text = questions.Count.ToString();

            questionContentSV.ChangeView(0, 0, 1, true);

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

                if(!SelectElement(index - 1, showDefault))
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
