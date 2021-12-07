using Todo.Data.Entities;
using Todo.Models.Gravatar;

namespace Todo.Models.TodoItems
{
    public class TodoItemSummaryViewmodel
    {
        public int TodoItemId { get; }
        public string Title { get; }
        public UserSummaryViewmodel ResponsibleParty { get; }
        public bool IsDone { get; }
        public Importance Importance { get; }
        public int Rank { get; }

        public GravatarProfile GravatarProfile { get; }

        public TodoItemSummaryViewmodel(int todoItemId, string title, bool isDone, UserSummaryViewmodel responsibleParty, Importance importance, int rank, GravatarProfile profile = null)
        {
            TodoItemId = todoItemId;
            Title = title;
            IsDone = isDone;
            ResponsibleParty = responsibleParty;
            Importance = importance;
            Rank = rank;
        }

        public string GetUserName()
        {
            if (GravatarProfile != null)
            {
                return GravatarProfile.DisplayName;
            }

            return ResponsibleParty.UserName;
        }
    }
}