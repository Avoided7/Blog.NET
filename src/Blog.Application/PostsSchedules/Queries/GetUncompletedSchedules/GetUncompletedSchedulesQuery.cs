using Blog.Application.Abstractions.Queries;
using Blog.Domain.Core;
using Blog.Domain.Primitives;

namespace Blog.Application.PostsSchedules.Queries.GetUncompletedSchedules;

public sealed record GetUncompletedSchedulesQuery(int Size) : IQuery<Result<IEnumerable<PostSchedule>>>;