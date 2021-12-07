using System.Collections.Generic;
using System.Linq;
using Todo.Data.Entities;
using Todo.EntityModelMappers.TodoItems;
using Todo.Models.TodoItems;
using Todo.Models.TodoLists;

namespace Todo.EntityModelMappers.TodoLists
{
    public static class TodoListDetailViewmodelFactory
    {
        public static TodoListDetailViewmodel Create(TodoList todoList, bool shouldSortByRank, bool hideDoneItems)
        {
            var itemsEnumerable = ApplyFiltering(todoList.Items, hideDoneItems).Select(TodoItemSummaryViewmodelFactory.Create);
            var items = ApplyOrdering(itemsEnumerable, shouldSortByRank).ToList();
            return new TodoListDetailViewmodel(todoList.TodoListId, todoList.Title, items, shouldSortByRank, hideDoneItems);
        }

        private static IEnumerable<TodoItem> ApplyFiltering(IEnumerable<TodoItem> items, bool hideDoneItems)
        {
            return items.Where(i => !hideDoneItems || !i.IsDone);
        }

        private static IOrderedEnumerable<TodoItemSummaryViewmodel> ApplyOrdering(IEnumerable<TodoItemSummaryViewmodel> items, bool shouldSortByRank)
        {
            var result = items.OrderBy(i => i.Importance);

            if (!shouldSortByRank)
            {
                return result;
            }

            return result.ThenBy(i => i.Rank);
        }
    }
}