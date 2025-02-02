﻿using System.Collections.Generic;
using Todo.Models.TodoItems;

namespace Todo.Models.TodoLists
{
    public class TodoListDetailViewmodel
    {
        public int TodoListId { get; }
        public string Title { get; }
        public ICollection<TodoItemSummaryViewmodel> Items { get; }
        public bool ShouldSortByRank { get; }
        public bool HideDoneItems { get; }

        public TodoListDetailViewmodel(int todoListId, string title, ICollection<TodoItemSummaryViewmodel> items, bool shouldSortByRank, bool hideDoneItems)
        {
            Items = items;
            TodoListId = todoListId;
            Title = title;
            ShouldSortByRank = shouldSortByRank;
            HideDoneItems = hideDoneItems;
        }
    }
}