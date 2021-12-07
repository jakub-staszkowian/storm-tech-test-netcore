using System;
using Todo.Data.Entities;
using Todo.Models.Gravatar;
using Todo.Models.TodoItems;
using Todo.Services;

namespace Todo.EntityModelMappers.TodoItems
{
    public static class TodoItemSummaryViewmodelFactory
    {
        public static TodoItemSummaryViewmodel Create(TodoItem ti)
        {
            var gravatarProfile = GetGravatarData(ti);
            return new TodoItemSummaryViewmodel(ti.TodoItemId, ti.Title, ti.IsDone, UserSummaryViewmodelFactory.Create(ti.ResponsibleParty), ti.Importance, ti.Rank, gravatarProfile);
        }

        private static GravatarProfile GetGravatarData(TodoItem item)
        {
            try
            {
                return Gravatar.GetUserData(item.ResponsiblePartyId);
            }
            catch (Exception e)
            {
                Console.WriteLine($"An exception occured while loading Gravatar data: {e.Message}");
                return null;
            }
        }
    }
}