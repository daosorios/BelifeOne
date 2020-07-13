using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

//adds 
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace BelifeWPf
{
    class DialogosMetro
    {
        internal static async void BasicDialog(string Text1, string Text2)
        {
            var Miconf = new MetroDialogSettings()
            {
                AffirmativeButtonText = "OK",
                ColorScheme = MetroDialogColorScheme.Accented
            };

            var metroWindow = (Application.Current.MainWindow as MetroWindow);
            await metroWindow.ShowMessageAsync(Text1, Text2, MessageDialogStyle.Affirmative,
                Miconf);
        }
        internal static async Task<MessageDialogResult> DialogTwoOptions(string positive, string negative, string Text1, string Text2)
        {
            var metroWindow = (Application.Current.MainWindow as MetroWindow);
            //configura las opciones del dialogo
            var Miconf = new MetroDialogSettings()
            {
                AffirmativeButtonText = positive,
                NegativeButtonText = negative,
                ColorScheme = MetroDialogColorScheme.Accented
            };

            var result =
            await metroWindow.ShowMessageAsync(
                Text1,
                Text2,
                MessageDialogStyle.AffirmativeAndNegative,
                Miconf
                );
            return result;
        }
    }
}
