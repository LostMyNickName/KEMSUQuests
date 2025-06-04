using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

//todo memento (Пока лень)

namespace Quest4
{
    [Serializable]
    public class TXT
    {

        public string Source { get; set; }

        public string Content { get; set; }

        public TXT(string source, FileStream fs)
        {
            StreamReader sr = new StreamReader(fs);
            Source = source;
            Content = sr.ReadToEnd();

        }

        public TXT() { }//Костыль для хмлсериалайзера

        public void Serialize(FileStream fs)
        {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, this);
            fs.Flush();
            fs.Close();
            Source = null;
            Content = null;
        }

        public void Deserialize(FileStream fs)
        {
            BinaryFormatter bf = new BinaryFormatter();
            TXT deserialized = (TXT)bf.Deserialize(fs);
            Source = deserialized.Source;
            Content = deserialized.Content;
            fs.Close();
        }

        public void xmlSerialize(FileStream fs)
        {
            XmlSerializer Xm = new XmlSerializer(typeof(TXT));
            Xm.Serialize(fs, this);
            fs.Flush();
            fs.Close();
            Source = null;
            Content = null;
        }

        public void xmlDeserialize(FileStream fs)
        {
            XmlSerializer xm = new XmlSerializer(this.GetType());
            TXT deserialized = (TXT)xm.Deserialize(fs);
            Source = deserialized.Source;
            Content = deserialized.Content;
            fs.Close();
        }

        public void Out(FileStream fs)
        {
            Console.WriteLine("Путь до файла: " + Source);
            Console.WriteLine("Содержимое файла:\n" + Content);

        }

        public void ReWriteFile(string source, string content)
        {
            Content = content;
            Source = source;
            StreamWriter sw = new StreamWriter(Source);
            sw.WriteLine(Content);
            sw.Flush();
            sw.Close();

        }

        public void WriteToFile(string source, string content)
        {
            Content += content;
            Source = source;
            StreamWriter sw = new StreamWriter(Source);
            sw.WriteLine(Content);
            sw.Flush();
            sw.Close();

        }

        public void DeleteFile(string source)
        {
            File.Delete(source);
        }

    }



    internal class Program
    {
        static int Main(string[] args)
        {
            //Console.WriteLine(Directory.GetCurrentDirectory());

            if (!Directory.Exists(Directory.GetCurrentDirectory() + "/SomeFiles"))
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/SomeFiles");
            if (!Directory.Exists(Directory.GetCurrentDirectory() + "/SerializedFiles"))
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/SerializedFiles");

            string WorkDirectory = Directory.GetCurrentDirectory() + "/SomeFiles";
            string SerializedDirectory = Directory.GetCurrentDirectory() + "/SerializedFiles";
            string[] AllFiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "/SomeFiles");
            //if (AllFiles.Length <= 0)
            //    Console.WriteLine("У вас нет ни одного файла");
            do
            {
                int choise = 0, SelectedFile = 0;
                Console.WriteLine("Выберите желаемое действие:\n" +
                    "1. Вывести список файлов\n" +
                    "2. Создать новый файл\n" +
                    "3. Вывести содержимое файла\n" +
                    "4. Перезаписать файл\n" +
                    "5. Дописать в конец файла\n" +
                    "6. Удалить файл\n" +
                    "7. Сериализовать бинарно туда-сюда\n" +
                    "8. Сериализовать хмльно туда-сюда\n" +
                    "9. Ну допустим кнопка ЕХ1Т");
                try
                {
                    choise = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("\nНу это же вообще не число :/\n");
                    choise = -1;
                }

                switch (choise)
                {
                    case 1:
                        AllFiles = Directory.GetFiles(WorkDirectory);
                        if (AllFiles.Length <= 0)
                            Console.WriteLine("У вас нет ни одного файла");
                        else
                            for (int i = 0; i < AllFiles.Length; ++i)
                                Console.WriteLine((i + 1) + ". " + AllFiles[i]);
                        break;
                    case 2:
                        Console.Write("Введите название файла: ");
                        string FileName = Console.ReadLine();
                        FileStream fs = new FileStream(WorkDirectory + "/" + FileName + ".txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                        TXT txt = new TXT(WorkDirectory + "/" + FileName + ".txt", fs);
                        fs.Flush();
                        fs.Close();
                        break;
                    case 3:
                        SelectedFile = 0;
                        Console.WriteLine("Выберите файл:");
                        AllFiles = Directory.GetFiles(WorkDirectory);
                        if (AllFiles.Length <= 0)
                            Console.WriteLine("У вас нет ни одного файла");
                        else
                            for (int i = 0; i < AllFiles.Length; ++i)
                                Console.WriteLine((i + 1) + ". " + AllFiles[i]);

                        Console.Write("Выбранный файл: ");
                        try
                        {
                            SelectedFile = Convert.ToInt32(Console.ReadLine());
                        }
                        catch
                        {
                            Console.WriteLine("\nНу это же вообще не число :/\n");
                            SelectedFile = 0;
                        }

                        for (int i = 0; i < AllFiles.Length; ++i)
                        {
                            if (i == SelectedFile - 1)
                            {
                                fs = new FileStream(AllFiles[i], FileMode.Open, FileAccess.Read);
                                txt = new TXT(AllFiles[i], fs);
                                txt.Out(fs);
                                fs.Flush();
                                fs.Close();
                            }
                        }
                        break;
                    case 4:
                        SelectedFile = 0;
                        Console.WriteLine("Выберите файл:");
                        AllFiles = Directory.GetFiles(WorkDirectory);
                        if (AllFiles.Length <= 0)
                            Console.WriteLine("У вас нет ни одного файла");
                        else
                            for (int i = 0; i < AllFiles.Length; ++i)
                                Console.WriteLine((i + 1) + ". " + AllFiles[i]);

                        Console.Write("Выбранный файл: ");
                        try
                        {
                            SelectedFile = Convert.ToInt32(Console.ReadLine());
                        }
                        catch
                        {
                            Console.WriteLine("\nНу это же вообще не число :/\n");
                            SelectedFile = 0;
                        }

                        for (int i = 0; i < AllFiles.Length; ++i)
                        {
                            if (i == SelectedFile - 1)
                            {
                                fs = new FileStream(AllFiles[i], FileMode.Open, FileAccess.ReadWrite);
                                txt = new TXT(AllFiles[i], fs);
                                fs.Flush();
                                fs.Close();
                                string content = "";
                                Console.WriteLine("Введите содержимое файла: ");
                                content = Console.ReadLine();
                                txt.ReWriteFile(AllFiles[i], content);
                                Console.WriteLine("Полученный файл: ");
                                txt.Out(fs);
                                fs.Flush();
                                fs.Close();
                            }
                        }
                        break;
                    case 5:
                        SelectedFile = 0;
                        Console.WriteLine("Выберите файл:");
                        AllFiles = Directory.GetFiles(WorkDirectory);
                        if (AllFiles.Length <= 0)
                            Console.WriteLine("У вас нет ни одного файла");
                        else
                            for (int i = 0; i < AllFiles.Length; ++i)
                                Console.WriteLine((i + 1) + ". " + AllFiles[i]);

                        Console.Write("Выбранный файл: ");
                        try
                        {
                            SelectedFile = Convert.ToInt32(Console.ReadLine());
                        }
                        catch
                        {
                            Console.WriteLine("\nНу это же вообще не число :/\n");
                            SelectedFile = 0;
                        }

                        for (int i = 0; i < AllFiles.Length; ++i)
                        {
                            if (i == SelectedFile - 1)
                            {
                                fs = new FileStream(AllFiles[i], FileMode.Open, FileAccess.ReadWrite);
                                txt = new TXT(AllFiles[i], fs);
                                fs.Flush();
                                fs.Close();
                                string content = "";
                                Console.WriteLine("Введите содержимое файла: ");
                                content = Console.ReadLine();
                                txt.WriteToFile(AllFiles[i], content);
                                Console.WriteLine("Полученный файл: ");
                                txt.Out(fs);
                                fs.Flush();
                                fs.Close();
                            }
                        }
                        break;
                    case 6:
                        SelectedFile = 0;
                        Console.WriteLine("Выберите файл:");
                        AllFiles = Directory.GetFiles(WorkDirectory);
                        if (AllFiles.Length <= 0)
                            Console.WriteLine("У вас нет ни одного файла");
                        else
                            for (int i = 0; i < AllFiles.Length; ++i)
                                Console.WriteLine((i + 1) + ". " + AllFiles[i]);

                        Console.Write("Выбранный файл: ");
                        try
                        {
                            SelectedFile = Convert.ToInt32(Console.ReadLine());
                        }
                        catch
                        {
                            Console.WriteLine("\nНу это же вообще не число :/\n");
                        }
                        for (int i = 0; i < AllFiles.Length; ++i)
                        {
                            if (i == SelectedFile - 1)
                            {
                                Console.WriteLine($"Файл {AllFiles[i]} удален!");
                                File.Delete(AllFiles[i]);
                            }
                        }
                        break;

                    case 7:
                        SelectedFile = 0;
                        Console.WriteLine("Выберите файл:");
                        AllFiles = Directory.GetFiles(WorkDirectory);
                        if (AllFiles.Length <= 0)
                            Console.WriteLine("У вас нет ни одного файла");
                        else
                            for (int i = 0; i < AllFiles.Length; ++i)
                                Console.WriteLine((i + 1) + ". " + AllFiles[i]);

                        Console.Write("Выбранный файл: ");
                        try
                        {
                            SelectedFile = Convert.ToInt32(Console.ReadLine());
                        }
                        catch
                        {
                            Console.WriteLine("\nНу это же вообще не число :/\n");
                        }
                        for (int i = 0; i < AllFiles.Length; ++i)
                        {
                            if (i == SelectedFile - 1)
                            {
                                fs = new FileStream(AllFiles[i], FileMode.OpenOrCreate, FileAccess.ReadWrite);
                                txt = new TXT(AllFiles[i], fs);
                                fs = new FileStream(SerializedDirectory + "/SerializedText.bin", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                                txt.Serialize(fs);
                                fs = new FileStream(SerializedDirectory + "/SerializedText.bin", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                                txt.Deserialize(fs);
                                fs = new FileStream(SerializedDirectory + "/SerializedText.bin", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                                txt.Out(fs);
                                fs.Flush();
                                fs.Close();
                            }
                        }
                        break;

                    case 8:
                        SelectedFile = 0;
                        Console.WriteLine("Выберите файл:");
                        AllFiles = Directory.GetFiles(WorkDirectory);
                        if (AllFiles.Length <= 0)
                            Console.WriteLine("У вас нет ни одного файла");
                        else
                            for (int i = 0; i < AllFiles.Length; ++i)
                                Console.WriteLine((i + 1) + ". " + AllFiles[i]);

                        Console.Write("Выбранный файл: ");
                        try
                        {
                            SelectedFile = Convert.ToInt32(Console.ReadLine());
                        }
                        catch
                        {
                            Console.WriteLine("\nНу это же вообще не число :/\n");
                        }
                        for (int i = 0; i < AllFiles.Length; ++i)
                        {
                            if (i == SelectedFile - 1)
                            {
                                fs = new FileStream(AllFiles[i], FileMode.OpenOrCreate, FileAccess.ReadWrite);
                                txt = new TXT(AllFiles[i], fs);
                                fs = new FileStream(SerializedDirectory + "/SerializedText.bin", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                                txt.xmlSerialize(fs);
                                fs = new FileStream(SerializedDirectory + "/SerializedText.bin", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                                txt.xmlDeserialize(fs);
                                fs = new FileStream(SerializedDirectory + "/SerializedText.bin", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                                txt.Out(fs);
                                fs.Flush();
                                fs.Close();

                            }
                        }
                        break;

                    case 9:
                        return 0;

                    default:
                        Console.WriteLine("Нет такого варианта");
                        break;
                }

                Console.WriteLine("\nДля продолжения нажмите любую клавишу...\n");
                Console.ReadKey(true);
            } while (true);

            //Как же я устал думать

        }
    }
}
