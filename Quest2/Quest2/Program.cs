using System;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using System.Linq;
using iText.Kernel.Pdf;
using HtmlAgilityPack;

namespace Quest2
{

	class AnyDocument
	{
		public string name;
		public string author;
		public string keywords;
		public string theme;
		public string size;
		public string path;

	}


	class WordDocument: AnyDocument
	{
		
	}

	class ExcelDocument: AnyDocument
	{

	}

	class TXTDocument: AnyDocument
	{

	}

	class PDF: AnyDocument
	{

	}

	class HTML: AnyDocument
	{

	}

	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Введите путь к файлу: ");
			string InputPath = Console.ReadLine();
			FileInfo fileinfo = new FileInfo(InputPath);
			
			if (fileinfo.Exists)
			{
				Console.WriteLine($"УРА! Введенный файл существует и имеет формат {fileinfo.Extension}\n");

				//word
				if (fileinfo.Extension == ".docx")
				{
					WordDocument doc = new WordDocument();
					
					doc.path = InputPath;
					doc.name = fileinfo.Name;
					doc.size = fileinfo.Length.ToString();
					using (WordprocessingDocument Doc = WordprocessingDocument.Open(doc.path, false))
					{
						doc.author = Doc.PackageProperties.Creator;
						doc.theme = Doc.PackageProperties.Subject;
						doc.keywords = Doc.PackageProperties.Keywords;
					}
					
					Console.WriteLine($"Имя файла: {doc.name}\nПуть к файлу: {doc.path}\n" +
						$"Размер файла = {doc.size} байт\nАвтор файла: {doc.author}\nТема документа: {doc.theme}\n" +
						$"Ключевые слова: {doc.keywords}");
				}

				//excel
				if (fileinfo.Extension == ".xlsx")
				{
					ExcelDocument doc = new ExcelDocument();

					doc.path = InputPath;
					doc.name = fileinfo.Name;
					doc.size = fileinfo.Length.ToString();
					using (WordprocessingDocument Doc = WordprocessingDocument.Open(doc.path, false))
					{
						doc.author = Doc.PackageProperties.Creator;		// По неизвестным мне причинам эксель не хочет сохранять автора документа
						doc.theme = Doc.PackageProperties.Subject;		// Но если сохранится, то на выводе обязательно будет честно-честно
						doc.keywords = Doc.PackageProperties.Keywords;
					}

					Console.WriteLine($"Имя файла: {doc.name}\nПуть к файлу: {doc.path}\n" +
						$"Размер файла = {doc.size} байт\nАвтор файла: {doc.author}\nТема документа: {doc.theme}\n" +
						$"Ключевые слова: {doc.keywords}");
				}

				//txt
				if (fileinfo.Extension == ".txt")
				{
					TXTDocument doc = new TXTDocument();

					doc.path = InputPath;
					doc.name = fileinfo.Name;
					doc.size = fileinfo.Length.ToString();
					
					string[] LinesOfTXT = File.ReadAllLines(doc.path);
					doc.author = LinesOfTXT.FirstOrDefault(l => l.StartsWith("Автор:"))?.Substring(6).Trim();
					doc.theme = LinesOfTXT.FirstOrDefault(l => l.StartsWith("Тема:"))?.Substring(5).Trim();
					doc.keywords = LinesOfTXT.FirstOrDefault(l => l.StartsWith("Теги:"))?.Substring(5).Trim();

					Console.WriteLine($"Имя файла: {doc.name}\nПуть к файлу: {doc.path}\n" +
						$"Размер файла = {doc.size} байт\nАвтор файла: {doc.author}\nТема документа: {doc.theme}\n" +
						$"Ключевые слова: {doc.keywords}");
				}

				//pdf
				if(fileinfo.Extension == ".pdf")
				{
					PDF doc = new PDF();

					doc.path = InputPath;
					doc.name = fileinfo.Name;
					doc.size = fileinfo.Length.ToString();

					PdfDocument pdf = new PdfDocument(new PdfReader(doc.path));
					doc.author = pdf.GetDocumentInfo().GetAuthor();
					doc.theme = pdf.GetDocumentInfo().GetSubject();
					doc.keywords = pdf.GetDocumentInfo().GetKeywords();

					Console.WriteLine($"Имя файла: {doc.name}\nПуть к файлу: {doc.path}\n" +
						$"Размер файла = {doc.size} байт\nАвтор файла: {doc.author}\nТема документа: {doc.theme}\n" +
						$"Ключевые слова: {doc.keywords}");
				}

				//HTML
				if(fileinfo.Extension == ".html")
				{
					HTML doc = new HTML();

					doc.path = InputPath;
					doc.name = fileinfo.Name;
					doc.size = fileinfo.Length.ToString();

					var htmldoc = new HtmlWeb().Load(doc.path);
					doc.theme = htmldoc.DocumentNode.SelectSingleNode("//title").InnerText;
					doc.author = htmldoc.DocumentNode.SelectSingleNode("//meta[@name = 'author']").GetAttributeValue("content", null);
					doc.keywords = htmldoc.DocumentNode.SelectSingleNode("//meta[@name = 'keywords']").GetAttributeValue("content", null);

					Console.WriteLine($"Имя файла: {doc.name}\nПуть к файлу: {doc.path}\n" +
						$"Размер файла = {doc.size} байт\nАвтор файла: {doc.author}\nТема документа: {doc.theme}\n" +
						$"Ключевые слова: {doc.keywords}");
				}
			}
			else
			{
				Console.WriteLine("\t\t!!!АХТУНГ!!!\nПо указанному пути не обнаржуен документ или путь указан неверно");
			}
		}
	}
}
