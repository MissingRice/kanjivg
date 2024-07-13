using System.Xml.Linq;

namespace KVGTools;

public static class FixKaishoNumbers {
    private const string FullPath = @"C:\Users\edwar\Programming\MonoGame\Projects\MonoGame-Kanji-Processor\KanjiVG\Kanji";
    private static readonly XNamespace svg = "http://www.w3.org/2000/svg";
    private static readonly XNamespace kvg = "http://kanjivg.tagaini.net";

    public static void Execute(int num, bool canWrite) {
        Dictionary<string, List<string>> groupedFiles = [];

        for(int i = 0; i < Math.Min(num == 0 ? int.MaxValue : num, TargetFiles.Count); i++)
            if(Path.GetFileNameWithoutExtension(TargetFiles[i]).Contains('-')){
                string cleanPath = GetOriginalFilePath(Path.Combine(FullPath, TargetFiles[i]));
                groupedFiles.TryAdd(cleanPath, new());
                groupedFiles[cleanPath].Add(Path.Combine(FullPath, TargetFiles[i]));
            } 
        
        foreach(var pair in groupedFiles)
            if(pair.Value.Any(str => str.Contains("Kaisho")))
                FixNumbers(pair, canWrite);
    }

    private static string GetOriginalFilePath(string path) {
        if(Path.GetFileNameWithoutExtension(path).Contains('-'))
            path = Path.Combine(Path.GetDirectoryName(path)!, Path.GetFileNameWithoutExtension(path).Split('-')[0] + ".svg");
        return path;
    }

    private static void FixNumbers(KeyValuePair<string, List<string>> pair, bool canWrite) {
        XDocument baseDoc = XDocument.Parse(File.ReadAllText(pair.Key));
        List<XElement> baseTextElements = baseDoc.Descendants(svg + "text").ToList();

        
        foreach(var path in pair.Value) {
            XDocument childDoc = XDocument.Parse(File.ReadAllText(path));
            List<XElement> childTextElements = childDoc.Descendants(svg + "text").ToList();
            List<XElement> newTextElements = [];

            for(int i = 0; i < baseTextElements.Count; i++) 
                if(i > childTextElements.Count - 1) 
                    newTextElements.Add(baseDoc.Descendants(svg + "text").Where(elem =>
                        elem.Value == (i + 1).ToString()).First());
            
            WriteFile(EditDocument(File.ReadAllLines(path).ToList(), newTextElements), path, canWrite);
        }
    }

    private static string EditDocument(List<string> file, List<XElement> textElements) {
        for(int i = 0; i < file.Count; i++)
            file[i] = file[i] + '\n';

        foreach(var elem in textElements)
            file.Insert(file.Count - 2, $"\t<text transform=\"{elem.Attribute("transform")!.Value}\">{elem.Value}</text>\n");
        return string.Concat(file);
    }

    private static void WriteFile(string file, string path, bool canWrite) {
        Console.WriteLine(file);

        if(!canWrite)
            return;
        
        File.WriteAllText(path, file);
    }

    private static readonly List<string> TargetFiles = [
        "05dd2-Kaisho.svg",
        "05f4e-Kaisho.svg",
        "06ade-Kaisho.svg",
        "06b12-Kaisho.svg",

        "07063-Kaisho.svg",
        "07cfa-Kaisho.svg",
        "07cfe-Kaisho.svg",
        "07d00-Kaisho.svg",
        "07d02-Kaisho.svg",
        "07d04-Kaisho.svg",
        "07d05-Kaisho.svg",
        "07d0b-Kaisho.svg",
        "07d0d-Kaisho.svg",
        "07d10-Kaisho.svg",
        "07d14-Kaisho.svg",
        "07d15-Kaisho.svg",
        "07d17-Kaisho.svg",
        "07d18-Kaisho.svg",
        "07d19-Kaisho.svg",
        "07d1a-Kaisho.svg",
        "07d1c-Kaisho.svg",
        "07d21-Kaisho.svg",
        "07d2c-Kaisho.svg",
        "07d30-Kaisho.svg",
        "07d32-Kaisho.svg",
        "07d33-Kaisho.svg",
        "07d35-Kaisho.svg",
        "07d39-Kaisho.svg",
        "07d3a-Kaisho.svg",
        "07d3f-Kaisho.svg",
        "07d42-Kaisho.svg",
        "07d43-Kaisho.svg",
        "07d44-Kaisho.svg",
        "07d45-Kaisho.svg",
        "07d46-Kaisho.svg",
        "07d4b-Kaisho.svg",
        "07d4c-Kaisho.svg",
        "07d4e-Kaisho.svg",
        "07d4f-Kaisho.svg",
        "07d50-Kaisho.svg",
        "07d56-Kaisho.svg",
        "07d5e-Kaisho.svg",
        "07d61-Kaisho.svg",
        "07d62-Kaisho.svg",
        "07d63-Kaisho.svg",
        "07d66-Kaisho.svg",
        "07d68-Kaisho.svg",
        "07d71-Kaisho.svg",
        "07d72-Kaisho.svg",
        "07d73-Kaisho.svg",
        "07d75-Kaisho.svg",
        "07d76-Kaisho.svg",
        "07d7d-Kaisho.svg",
        "07d89-Kaisho.svg",
        "07d8f-Kaisho.svg",
        "07d93-Kaisho.svg",
        "07d99-Kaisho.svg",
        "07d9a-Kaisho.svg",
        "07d9b-Kaisho.svg",
        "07d9c-Kaisho.svg",
        "07d9f-Kaisho.svg",
        "07da2-Kaisho.svg",
        "07da3-Kaisho.svg",
        "07dab-Kaisho.svg",
        "07dac-Kaisho.svg",
        "07dad-Kaisho.svg",
        "07daf-Kaisho.svg",
        "07db0-Kaisho.svg",
        "07db1-Kaisho.svg",
        "07db4-Kaisho.svg",
        "07db5-Kaisho.svg",
        "07db8-Kaisho.svg",
        "07dba-Kaisho.svg",
        "07dbb-Kaisho.svg",
        "07dbd-Kaisho.svg",
        "07dbd-KaishoHzFst.svg",
        "07dbe-Kaisho.svg",
        "07dbf-Kaisho.svg",
        "07dc7-Kaisho.svg",
        "07dcb-Kaisho.svg",
        "07dcb-KaishoMdFst.svg",
        "07dcf-Kaisho.svg",
        "07dd1-Kaisho.svg",
        "07dd2-Kaisho.svg",
        "07dd5-Kaisho.svg",
        "07dd8-Kaisho.svg",
        "07dda-Kaisho.svg",
        "07ddd-Kaisho.svg",
        "07ddd-KaishoVt3.svg",
        "07dde-Kaisho.svg",
        "07de0-Kaisho.svg",
        "07de1-Kaisho.svg",
        "07de4-Kaisho.svg",
        "07de8-Kaisho.svg",
        "07de9-Kaisho.svg",
        "07dec-Kaisho.svg",
        "07def-Kaisho.svg",
        "07df2-Kaisho.svg",
        "07df4-Kaisho.svg",
        "07e01-Kaisho.svg",
        "07e04-Kaisho.svg",
        "07e05-Kaisho.svg",
        "07e09-Kaisho.svg",
        "07e0a-Kaisho.svg",
        "07e0b-Kaisho.svg",
        "07e12-Kaisho.svg",
        "07e12-KaishoVtLst.svg",
        "07e1b-Kaisho.svg",
        "07e1f-Kaisho.svg",
        "07e21-Kaisho.svg",
        "07e26-Kaisho.svg",
        "07e2e-Kaisho.svg",
        "07e31-Kaisho.svg",
        "07e32-Kaisho.svg",
        "07e35-Kaisho.svg",
        "07e37-Kaisho.svg",
        "07e39-Kaisho.svg",
        "07e3a-Kaisho.svg",
        "07e3d-Kaisho.svg",
        "07e3e-Kaisho.svg",
        "07e3e-KaishoVtLst.svg",
        "07e43-Kaisho.svg",
        "07e46-Kaisho.svg",
        "07e4a-Kaisho.svg",
        "07e4d-Kaisho.svg",
        "07e54-Kaisho.svg",
        "07e55-Kaisho.svg",
        "07e56-Kaisho.svg",
        "07e59-Kaisho.svg",
        "07e5a-Kaisho.svg",
        "07e5d-Kaisho.svg",
        "07e5e-Kaisho.svg",
        "07e66-Kaisho.svg",
        "07e67-Kaisho.svg",
        "07e69-Kaisho.svg",
        "07e6a-Kaisho.svg",
        "07e70-Kaisho.svg",
        "07e79-Kaisho.svg",
        "07e7b-Kaisho.svg",
        "07e7d-Kaisho.svg",
        "07e7f-Kaisho.svg",
        "07e7f-KaishoHzFst.svg",
        "07e83-Kaisho.svg",
        "07e83-KaishoVt12.svg",
        "07e88-Kaisho.svg",
        "07e89-Kaisho.svg",
        "07e8c-Kaisho.svg",
        "07e8f-Kaisho.svg",
        "07e8f-KaishoVtLst.svg",
        "07e90-Kaisho.svg",
        "07e92-Kaisho.svg",
        "07e92-KaishoVtLst.svg",
        "07e93-Kaisho.svg",
        "07e9c-Kaisho.svg",
        "07e9c-KaishoHzFst.svg",
        "07f82-Kaisho.svg",
        "07f85-Kaisho.svg",
        "081e0-Kaisho.svg",
        "0846f-Kaisho.svg",
        "0860a-Kaisho.svg",
        "08630-Kaisho.svg",
        "0863f-Kaisho.svg",
        "0883b-Kaisho.svg",
        "08b8a-Kaisho.svg",
        "08f61-Kaisho.svg",
        "0908f-Kaisho.svg",
        "0947c-Kaisho.svg",
        "0947e-Kaisho.svg",];
}