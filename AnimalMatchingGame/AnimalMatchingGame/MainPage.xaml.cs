namespace AnimalMatchingGame
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            AnimalButtons.IsVisible = false;
        }

        private void PlayAgainButton_Clicked(object sender, EventArgs e)
        {
            AnimalButtons.IsVisible = true;
            PlayAgainButton.IsVisible = false;

            List<string> animalEmoji = new List<string>()
            {
                "🐶", "🐶",
                "🐈", "🐈",
                "❤️", "❤️",
                "👑", "👑",
                "🌷", "🌷",
                "🐘", "🐘",
                "🦘", "🦘",
                "🍎", "🍎",
            };

            foreach(var button in AnimalButtons.Children.OfType<Button>())
            {
                int index = Random.Shared.Next(animalEmoji.Count);
                string nextEmoji = animalEmoji[index];
                button.Text = nextEmoji;
                animalEmoji.RemoveAt(index);
            }
            //Add causes to the app to start a timer that execute a method called TimerTick every 0.1 a second.
            Dispatcher.StartTimer(TimeSpan.FromSeconds(.1), TimerTick);
        }

        int tenthsOfSecondsElapsed = 0;
        private bool TimerTick()
        {
            //If you close your app the timer could still tick after the TimeElapsed lable disappears
            //which could cause an error. This statement keeps that from happening
            if (!this.IsLoaded) return false;
            //The time ticks every 10th of a second. Adidng 1 to this field keeps track of how many of those 10ths have elapsed
            tenthsOfSecondsElapsed++;
            //This statement updates the TimeElapsed lable with the latest time, dividing the 10ths of second by 10 to convert it to seconds
            TimeElapsed.Text = "Time elapsed: " +
                (tenthsOfSecondsElapsed / 10f).ToString("0.0s");
            //If the "Play Again" button is visible again, that means the game is over the timer can stop running.
            //The if statement runs the next two statements only if the game is running
            if (PlayAgainButton.IsVisible)
            {
                //We need to reset the 10ths of seconds counter so it starts at 0 the next time the game starts
                tenthsOfSecondsElapsed = 0;
                //This statement causes the timer to stop, and no other statements in the method get executed
                return false;
            }
            //This statement is only executed if the if statement didn't find the "Play again?" button visible. It tells the timer to keep running
            return true;
        }

        Button lastClicked;
        bool findingMatch = false;
        int matchesFound;
        private void Button_Clicked(object sender, EventArgs e)
        {
            if(sender is Button buttonClicked)
            {
                if(!string.IsNullOrEmpty(buttonClicked.Text) && findingMatch == false)
                {
                    buttonClicked.BackgroundColor = Colors.Red;
                    lastClicked = buttonClicked;
                    findingMatch = true;
                }
                else
                {
                    if(buttonClicked != lastClicked && buttonClicked.Text == lastClicked.Text && buttonClicked.Text != "")
                    {
                        matchesFound++;
                        lastClicked.Text = "";
                        buttonClicked.Text = "";
                    }
                    lastClicked.BackgroundColor = Colors.LightBlue;
                    buttonClicked.BackgroundColor = Colors.LightBlue;
                    findingMatch = false;
                }
            }
            if(matchesFound == 8)
            {
                matchesFound = 0;
                AnimalButtons.IsVisible = false;
                PlayAgainButton.IsVisible = true;
            }
        }
    }
}