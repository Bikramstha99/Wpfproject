using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private const string FilePath = @"D:\PlayerData\PlayersData.txt";
        public class Player
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
            public string Team { get; set; }
        }
        private void SavePlayerButton_Click(object sender, RoutedEventArgs e)
        {
            // Load existing players from the file
            List<Player> players = LoadPlayersFromFile(FilePath);

            // Determine the next available ID
            int nextId = players.Any() ? players.Max(p => p.Id) + 1 : 1;

            // Create a new player object
            Player newPlayer = new Player
            {
                Id = nextId,
                Name = PlayerNameTextBox.Text,
                Age = int.TryParse(AgeTextBox.Text, out int age) ? age : 0,
                Team = TeamTextBox.Text
            };

            string directoryPath = @"D:\PlayerData";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // Add the new player to the list
            players.Add(newPlayer);

            // Save the updated list back to the file
            SavePlayersToFile(FilePath, players);

            // Show confirmation
            MessageBox.Show("Player saved successfully!", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private List<Player> LoadPlayersFromFile(string filePath)
        {
            if (!File.Exists(filePath))
                return new List<Player>();

            string jsonData = File.ReadAllText(filePath);
            return string.IsNullOrWhiteSpace(jsonData)
            ? new List<Player>()
                : System.Text.Json.JsonSerializer.Deserialize<List<Player>>(jsonData);
        }

        private void SavePlayersToFile(string filePath, List<Player> players)
        {
            string jsonData = System.Text.Json.JsonSerializer.Serialize(players, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, jsonData);
        }

        private void PlayerNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TeamTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}