using System;
using System.Media;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace RussianRoulete
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        SoundPlayer gunshot = new SoundPlayer(@"C:\Users\Admin\source\repos\RussianRoulete\RussianRoulete\Sounds\gunshot.wav");

        Random r = new Random();

        int selctedChamber;

        int RollAllCount = 0;
        int RollCount = 0;
        int RollLucky = 0;
        int RollUnlucky = 0;

        int numSleep = 75;

        String str = "Крутим";

        public MainWindow()
        {
            InitializeComponent();
        }

        async void Button_Click(object sender, RoutedEventArgs e)
        {
            RollCylynder();

            Stats();

        }

        async Task RollCylynder()
        {

            int GeneratedValue;
            int rn;

            GeneratedValue = r.Next(12, 37);
            rn = r.Next(2, 6);


            for (int i = 1; i <= GeneratedValue; i++)
            {
                if (i >= (GeneratedValue - rn)) numSleep = 350;


                if (str == "Крутим...") str = "Крутим";

                if (selctedChamber == 6) selctedChamber = 0;
               
                selctedChamber++;

                switch(selctedChamber)
                {
                    case 1:
                        Chamber1.IsChecked = true;
                        break;
                    case 2:
                        Chamber2.IsChecked = true;
                        break;
                    case 3:
                        Chamber3.IsChecked = true;
                        break;
                    case 4:
                        Chamber4.IsChecked = true;
                        break;
                    case 5:
                        Chamber5.IsChecked = true;
                        break;
                    case 6:
                        Chamber6.IsChecked = true;
                        break;
                }
                RollBT.IsEnabled = false;

                str += ".";
                setLB(str, 0, 0, 0);

                await Task.Delay(numSleep);



            }

           
            if (Chamber1.IsChecked != true)
            {
                setLB("Щёлк, Вам повезло!", 0, 100, 0);

                LuckStats();

                RollBT.IsEnabled = true; 
            }
            else
            {
                gunshot.Play();

                setLB("БАМ, Смерть! Вы проиграли.", 139, 0, 0);

                UnLuckStats();

/*----------------------Перезапуск игры ----------------------------------*/

                await Task.Delay(3000);


                Chamber1.IsChecked = false;

                EnableRadioB(true);

                setLB("Вставьте патрон в цилиндр", 0, 0, 0);

            }
            numSleep = 75;
        }


        private void Chamber_Loaded(object sender, RoutedEventArgs e)
        {


            if (Chamber1.IsChecked == true) selctedChamber = 1;
            else if (Chamber2.IsChecked == true) selctedChamber = 2;
            else if (Chamber3.IsChecked == true) selctedChamber = 3;
            else if (Chamber4.IsChecked == true) selctedChamber = 4;
            else if (Chamber5.IsChecked == true) selctedChamber = 5;
            else if (Chamber6.IsChecked == true) selctedChamber = 6;

            EnableRadioB(false);

            setLB("Начинайте крутить", 0, 0, 0);

            RollBT.IsEnabled = true;
        }


        void LuckStats()
        {
            RollLucky++;

            LLuck.Content = "Удачные прокруты: ";
            LLuck.Content += RollLucky.ToString();
        }

        void UnLuckStats()
        {
            RollUnlucky++;

            LUnluck.Content = "Неудачные прокруты: ";
            LUnluck.Content += RollUnlucky.ToString();

            RollCount = 0;
        }

        void Stats()
        {
            LRollsAll.Content = "Всего прокручено: ";
            LRolls.Content = "Прокручено сейчас: ";

            RollAllCount++;
            RollCount++;

            LRollsAll.Content += RollAllCount.ToString();
            LRolls.Content += RollCount.ToString();
        }

        void setLB(String str, byte r, byte g , byte b)
        {
            LText.Content = str;
            LText.Foreground = new SolidColorBrush(Color.FromRgb(r, g, b)); 
        }

        void EnableRadioB(bool setChecked)
        {
            Chamber1.IsEnabled = setChecked;
            Chamber2.IsEnabled = setChecked;
            Chamber3.IsEnabled = setChecked;
            Chamber4.IsEnabled = setChecked;
            Chamber5.IsEnabled = setChecked;
            Chamber6.IsEnabled = setChecked;
        }
    }
}
