using QVC.TASK.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QVC.TASK.BL
{
    public interface IBaseBL<T>
    {
        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách đối tượng bản ghi</returns>
        /// Created by: QVCANH (28/11/2022)
        public List<T> GetAll(string domain);

        /// <summary>
        /// Lấy danh sách bản ghi theo id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="domaindb"></param>
        /// <returns></returns>
        public List<T> GetAllById(string id, string domaindb);
        public T GetById(string id, string domaindb);

        /// <summary>
        /// >Thêm mới 1 bản ghi
        /// </summary>
        /// <param name="dataInsert"></param>
        /// <returns></returns>
        public int Insert(DataInsert<T> dataInsert);
        public int Update(DataInsert<T> data);
        public int Delete(Guid id, string db);
    }
}
