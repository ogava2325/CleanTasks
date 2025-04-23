using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Api.Hubs;

[Authorize]
public class ProjectHub : Hub
{
    public Task JoinProjectGroup(string projectId) =>
        Groups.AddToGroupAsync(Context.ConnectionId, projectId);

    public Task LeaveProjectGroup(string projectId) =>
        Groups.RemoveFromGroupAsync(Context.ConnectionId, projectId);
}