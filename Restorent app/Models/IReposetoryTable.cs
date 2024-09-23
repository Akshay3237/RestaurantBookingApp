using System.Collections.Generic;

namespace Restorent_app.Models
{
    public interface IReposetoryTable
    {
        public TableModel addTable(TableModel table);

        public TableModel removeTable(TableModel table);

        public TableModel getTableModelByTableId(int id);

        public List<TableModel> getTableModels();

        public List<TableModel> getTableModelsByRestaurantId(int id);

        public bool UpdateTableById(TableModel table);



    }
}
