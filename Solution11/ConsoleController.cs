using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution11
{
    internal class ConsoleController
    {
        private void _outputParticipant (int index, List<Item> participants)
        {
            Console.WriteLine(participants[index].FirstName + " | " + participants[index].LastName + " | " + participants[index].RegistrationDate.ToString("dd/MM/yyyy HH:mm") + " | " + participants[index].Service);
        }
        private void _writeConsoleDelimiter()
        {
            Console.WriteLine("------------------------------------------------------------");
        }
        public void launchProgram ()
        {
            Parsing p = new Parsing();

            _getCommands(p.parseParticipants());
        }
        private void _getCommands(List<Item> participants)
        {
            Console.Write("->");
            string input = Console.ReadLine();
            string command = string.Empty;
            string p = string.Empty;
            int i = 0;

            _writeConsoleDelimiter();
            while (i < input.Length && input[i] != ' ')
            {
                command += input[i];
                i++;
            }
            i++;
            while (i < input.Length)
            {
                p += input[i];
                i++;
            }
            switch (command)
            {
                case "get-page":
                    try
                    {
                        int intP = Convert.ToInt32(p);
                        for (int j = intP * 4 - 4; j <= intP * 4; j++)
                        {
                            if (j < participants.Count)
                                _outputParticipant(j, participants);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Поиск страницы с введенным параметром невозможен");
                    }
                    _writeConsoleDelimiter();
                    _getCommands(participants);
                    break;
                case "search":
                    bool isFound = false;
                    for (int k = 0; k < participants.Count; k++)
                    {
                        if (participants[k].FirstName.Contains(p) || participants[k].LastName.Contains(p))
                        {
                            _outputParticipant(k, participants);
                            isFound = true;
                        }
                    }
                    if (!isFound)
                        Console.WriteLine("По запросу ничего не найдено");
                    _writeConsoleDelimiter();
                    _getCommands(participants);
                    break;
                case "clear":
                    Console.Clear();
                    _getCommands(participants);
                    break;
                case "quit":
                    break;
                case "help":
                    Console.WriteLine("get-page номер_страницы - вывод данной страницы\nsearch имя/фамилия - поиск по введенному имени или фамилии\nclear - очистка консоли\nquit - выход из программы");
                    _writeConsoleDelimiter();
                    _getCommands(participants);
                    break;
                default:                    
                    Console.WriteLine("Неизвестная команда. Попробуйте снова\nВведите help для просмотра списка командd");
                    _writeConsoleDelimiter();
                    _getCommands(participants);
                    break;
            }
        }
    }
}
