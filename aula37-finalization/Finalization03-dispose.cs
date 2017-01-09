using System;
using System.IO;
using System.Threading;

static class App{

    class FileStreamClean : FileStream{
        public FileStreamClean(string path) : base(path, FileMode.Create) {}
        ~FileStreamClean()
        {
            Thread.Sleep(1000);
            Console.WriteLine("Finalization finished!");
            // Chamada ao base::Finalize() => GC.SupressFinalize(this) => Remove da Finalization List
        }
    }

    static void PrintRunningGC(){
        Console.WriteLine("Running GC...");
        GC.Collect();
        GC.WaitForPendingFinalizers();
    }

	public static void Main(){
        // 
        // Uma instância de FileStream mantém um handle para um recurso nativo, i.e. um ficheiro.
        //
        using(FileStream fs = new FileStreamClean("out.txt")) {
            // Wait for user to hit <Enter>
            Console.WriteLine("Wait for user to hit <Enter>");
            Console.ReadLine();
        }
        /* <=>
        FileStream fs = null;
        try{
            fs = new FileStreamClean("out.txt");
            // Wait for user to hit <Enter>
            Console.WriteLine("Wait for user to hit <Enter>");
            Console.ReadLine();
        } finally {
            fs.Dispose();
        }*/
        
        Console.WriteLine("Filestream disposed....");
        
        // PrintRunningGC();
        Console.WriteLine("Wait for user to hit <Enter>");
		Console.ReadLine();
        
	}
    
}
