using System;
using System.IO;
using System.Text;

namespace FileManipulation
{
    /// <summary>
    /// Examples on how to manipulate files
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // AppDomain.CurrentDomain.BaseDi‌​rectory place us in [project]/bin/[Debug | Release]/
            var currentPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDi‌​rectory, "..\\..\\"));

            // Path to the test file
            var pathToTestFile = @"C:\00 - Data\programmation\TestSolution\FileManipulation\Resources\TestFile.txt";
            #region file opening
            #region File.Open(String, FileMode)
            // FileMode.Open : assume that the file exists. If it doesn't -> exception!
            FileStream fsSimplestOpen = File.Open(pathToTestFile, FileMode.Open);

            // Try to open it again: error, the file is already opened
            //FileStream fsSimplestOpen2 = File.Open(pathToTestFile, FileMode.Open);

            // Close it to release the resource
            fsSimplestOpen.Close();

            // This, time, it can be open again
            FileStream fsSimplestOpen3 = File.Open(pathToTestFile, FileMode.Open);

            // Close it to free the file
            fsSimplestOpen3.Close();

            // Proper way
            using (FileStream fs = File.Open(pathToTestFile, FileMode.Open))
            {
                // Manipulate the stream
            }

            // The resource is automatically freed
            FileStream fsSimplestOpen4 = File.Open(pathToTestFile, FileMode.Open);
            fsSimplestOpen4.Close();

            // If we don't want to raise an exception if the file doesn't exist, we create the file
            using (FileStream fs = File.Open((pathToTestFile + "tt"), FileMode.OpenOrCreate))
            {
                // manipulate the stream
            }
            #endregion File.Open(String, FileMode)

            #region File.Open(String, FileMode, FileAccess)
            // Slighty the same thing as File.Open(String, FileMode); we specify operations that
            // can be done on the file

            using (FileStream fs = File.Open(pathToTestFile, FileMode.Open, FileAccess.Read))
            {
                // In this example, we assume that the file is less than byte[int32.MaxValue] in length (approx. 2 GB)
                // because we want to read the whole file at once; the Read method can only take an int as blocksize.
                // If the file is bigger, we have to deal with the "seek" method
                // -> cf. https://stackoverflow.com/questions/8920552/how-to-read-a-large-file-more-than-2-gb-using-filestream

                // We're gonna read the whole file at once.
                int blockSize = (int)fs.Length;
                var buffer = new byte[fs.Length];
                int readCount;
                int offset = 0;

                // Read until the end of the fileStream. In practice the loop will be processed
                // only one time because blockSize is the size of the file
                while ((readCount = fs.Read(buffer, offset, blockSize - offset)) > 0)
                {
                    offset += readCount;
                }

                // Reposition the stream at file start
                fs.Seek(0, SeekOrigin.Begin);

                // If we try to write in the file while the file access is set to read: error
                //var dataToWrite = "I want to be written in a file";
                ////var dataToWriteAsByte = new UTF8Encoding(true).GetBytes(dataToWrite);
                //var dataToWriteAsByte = Encoding.UTF8.GetBytes(dataToWrite);
                //fs.Write(dataToWriteAsByte, 0, dataToWriteAsByte.Length); // => error
            }

            // This time in write mode
            using (FileStream fs = File.Open(pathToTestFile, FileMode.Open, FileAccess.Write))
            {
                // the write is possible
                var dataToWrite = "I want to be written in a file and I've got special chars: é è ë ç ¤¡²³¤€¼½¾‘";
                //var dataToWriteAsByte = new UTF8Encoding(true).GetBytes(dataToWrite);
                var dataToWriteAsByte = Encoding.UTF8.GetBytes(dataToWrite);
                fs.Write(dataToWriteAsByte, 0, dataToWriteAsByte.Length);

                // The data are flushed from the stream to the file; after that, if an exception is
                // raised, the file contains the data
                fs.Flush();

                // Reposition the stream at file start
                fs.Seek(0, SeekOrigin.Begin);

                // The read isn't possible
                //int blockSize = (int)fs.Length;
                //var buffer = new byte[fs.Length];
                //int readCount;
                //int offset = 0;
                //while ((readCount = fs.Read(buffer, offset, blockSize - offset)) > 0) // => error
                //{
                //    offset += readCount;
                //}
            }

            // Read and write: there's a mode for that!
            using (FileStream fs = File.Open(pathToTestFile, FileMode.Open, FileAccess.ReadWrite))
            {
                // the write is possible
                var dataToWrite = "I want to be written in a file";
                //var dataToWriteAsByte = new UTF8Encoding(true).GetBytes(dataToWrite);
                var dataToWriteAsByte = Encoding.UTF8.GetBytes(dataToWrite);
                fs.Write(dataToWriteAsByte, 0, dataToWriteAsByte.Length);

                // The data are flushed from the stream to the file; after that, if an exception is
                // raised, the file contains the data
                fs.Flush();

                // Reposition the stream at file start
                fs.Seek(0, SeekOrigin.Begin);

                // The read is also possible
                int blockSize = (int)fs.Length;
                var buffer = new byte[fs.Length];
                int readCount;
                int offset = 0;
                while ((readCount = fs.Read(buffer, offset, blockSize - offset)) > 0)
                {
                    offset += readCount;
                }
            }
            #endregion File.Open(String, FileMode, FileAccess)

            #region File.Open(String, FileMode, FileAccess, FileShare)
            // Same thing as File.Open(String, FileMode, FileAccess); this time, we specify access
            // to the file that is processed.
            // Without this parameter, the file is automatically denied access while it's used
            using (FileStream fs = File.Open(pathToTestFile, FileMode.Open, FileAccess.Write, FileShare.None))
            {
                // FileShare.None : default value for File.Open(String, FileMode, FileAccess)

                Console.WriteLine($"the file {pathToTestFile} is blocked until you press a key");
                Console.ReadKey();
            }

            using (FileStream fs = File.Open(pathToTestFile, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                Console.WriteLine($"the file {pathToTestFile} can still be read and written to");
                Console.ReadKey();
            }

            #endregion File.Open(String, FileMode, FileAccess, FileShare)

            #region File.ReadAllBytes
            byte[] contentAsByte = File.ReadAllBytes(pathToTestFile);
            string contentAsString = Encoding.UTF8.GetString(contentAsByte);
            #endregion File.ReadAllBytes

            #region File.ReadAllLines
            // Return all lines as a string[]. The WHOLE file is loaded and then the array is available;
            // for big files, it's not recommanded => time and memory
            // See https://stackoverflow.com/questions/21969851/what-is-the-difference-between-file-readlines-and-file-readalllines
            string[] lines = File.ReadAllLines(pathToTestFile);

            Console.WriteLine("-------- foreach on File.ReadAllLines -------- ");
            foreach (var line in File.ReadAllLines(pathToTestFile))
            {
                Console.WriteLine(line);
            }
            #endregion File.ReadAllLines

            #region File.ReadLines
            // Return all lines as a IEnumerable<string>; can be iterated over contrary to File.ReadAllLines
            // the file is not loaded at once; each line is read one after another => better for big files
            Console.WriteLine("-------- foreach on File.ReadLines -------- ");
            foreach (var line in File.ReadLines(pathToTestFile))
            {
                Console.WriteLine(line);
            }
            #endregion File.ReadLines
            #endregion file opening

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
