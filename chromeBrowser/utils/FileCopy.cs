using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace chromeBrowser
{
    class FileCopy
    {
        public FileCopy()
        {

        }

        public void Copy()
        {
            try
            {
                byte[] buffer = new byte[1024 * 1024]; // 1MB buffer
                bool cancelFlag = false;

                using (FileStream source = new FileStream(SourceFilePath, FileMode.Open, FileAccess.Read))
                {
                    long fileLength = source.Length;
                    
                    using (FileStream dest = new FileStream(DestFilePath, FileMode.CreateNew, FileAccess.Write))
                    {
                        long totalBytes = 0;
                        int currentBlockSize = 0;
                        while ((currentBlockSize = source.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            totalBytes += currentBlockSize;
                            double persentage = (double)totalBytes * 100.0 / fileLength;
                            if (Parent != null)
                            {
                                Parent.setloadingPercentValue(persentage);
                                Parent.setLoad(totalBytes, fileLength);
                            }
                            dest.Write(buffer, 0, currentBlockSize);
                            cancelFlag = false;                            
                            if (cancelFlag == true)
                            {
                                break;
                            }
                            System.Threading.Thread.Sleep(100);
                        }
                    }
                }
                //OnComplete();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            //completeCopy();
        }


        public void copySet(string Source, string Dest)
        {
            this.SourceFilePath = Source;
            this.DestFilePath = Dest;
            //this.Parent = this;
            //OnProgressChanged += delegate { };
            //OnComplete += delegate { };
        }


        public void setParent(fileLoadProgress  fl)
        {
            this.Parent = fl;
        }

        public string SourceFilePath { get; set; }
        public string DestFilePath { get; set; }

        public fileLoadProgress Parent { get; set; }
    }

     
}
