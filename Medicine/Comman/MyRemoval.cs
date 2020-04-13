using DataModel.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class MyRemoval<T>
    {
        /// <summary>
        /// 数据去重
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static void MyInventoryModelsRemoval(List<InventoryModels> list)
        {
            List<InventoryModels> List = list;
            //数组去重的思路 【1】先创建一个新的List集合
            List<InventoryModels> listModel = new List<InventoryModels>();
            //【2】先遍历循环查询到的数据
            foreach (var item in List)
            {
                //判断创建的新的List集合是否有数据，如果没有数据，则进入if代码块
                if (listModel.Count == 0)
                {
                    //向新的List集合中添加数据
                    listModel.Add(item);
                }
                //声明一个变量，作用是：当作一个标记
                int count = 0;
                //循环变量新的List集合
                foreach (var item1 in listModel)
                {
                    //判断：新的List集合中的PowerID包含查到List集合的PowerID，如果包含，进入if代码块中修改标记
                    if (item1.ID.ToString().Contains(item.ID.ToString()))
                    {
                        //修改标记
                        count = 1;
                    }
                }
                //判断：如果标记没有被修改，也就是没有重复，进入代码块中添加数据
                if (count == 0)
                {
                    //向新的List集合中添加数据
                    listModel.Add(item);
                }
            }
        }
    }
}
