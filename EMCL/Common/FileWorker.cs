using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMCL.Common
{
    class FileWorker
    {
        /// <summary>
        /// 删除指定目录下的指定后缀名的文件
        /// </summary>
        /// <param name="directory">要删除的文件所在的目录，是绝对目录，如d:\temp</param>
        /// <param name="masks">要删除的文件的后缀名的一个数组，比如masks中包含了.cs,.vb,.c这三个元素</param>
        /// <param name="searchSubdirectories">表示是否需要递归删除，即是否也要删除子目录中相应的文件</param>
        /// <param name="ignoreHidden">表示是否忽略隐藏文件</param>
        /// <param name="deletedFileCount">表示总共删除的文件数</param>
        public static void DeleteFiles(string directory, string[] masks, bool searchSubdirectories, bool ignoreHidden)
        {
            //先删除当前目录下指定后缀名的所有文件
            foreach (string file in Directory.GetFiles(directory, "*.*"))
            {
                if (!(ignoreHidden && (File.GetAttributes(file) & FileAttributes.Hidden) == FileAttributes.Hidden))
                {
                    foreach (string mask in masks)
                    {
                        if (Path.GetExtension(file) == mask)
                        {
                            File.Delete(file);
                        }
                    }
                }
            }

            //如果需要对子目录进行处理，则对子目录也进行递归操作
            if (searchSubdirectories)
            {
                string[] childDirectories = Directory.GetDirectories(directory);
                foreach (string dir in childDirectories)
                {
                    if (!(ignoreHidden && (File.GetAttributes(dir) & FileAttributes.Hidden) == FileAttributes.Hidden))
                    {
                        DeleteFiles(dir, masks, searchSubdirectories, ignoreHidden);
                    }
                }
            }
        }
    }
}
