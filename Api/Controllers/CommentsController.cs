using System.Security.Claims;
using Application.Common.Dtos;
using Application.Features.Comment.Commands.CreateComment;
using Application.Features.Comment.Commands.DeleteComment;
using Application.Features.Comment.Commands.UpdateComment;
using Application.Features.Comment.Queries.GetCommentsByCardId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CommentsController(IMediator mediator) : ControllerBase
{
        
    // GET: api/<CommentsController>
    [HttpGet("{cardId:guid}")]
    public async Task<IEnumerable<CommentDto>> Get(Guid cardId)
    {
        var query = new GetCommentsByCardIdQuery(cardId);
        var comments = await mediator.Send(query);
        return comments;
    }
        
    // POST api/<CommentsController>
    [HttpPost]
    public async Task<ActionResult> Post(CreateCommentCommand command)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
        command.UserId = Guid.Parse(userId);
            
        var comment = await mediator.Send(command);

        return Ok(comment);
    }

    // PUT api/<CommentsController>/5
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put(Guid id, UpdateCommentCommand command)
    {
        if (id != command.Id)
        {
            BadRequest();
        }

        await mediator.Send(command);
        return NoContent();
    }

    // DELETE api/<CommentsController>/5
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteCommentCommand(id);

        await mediator.Send(command);
        return NoContent();
    }
}