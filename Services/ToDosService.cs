using TodoApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace TodoApi.Services;

public class TodosService
{
    private readonly IMongoCollection<TodoItem> _todosCollection;

    public TodosService(
        IOptions<TodoDbSettings> todoDbSettings)
    {
        var mongoClient = new MongoClient(
            todoDbSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            todoDbSettings.Value.DatabaseName);

        _todosCollection = mongoDatabase.GetCollection<TodoItem>(
            todoDbSettings.Value.TodoCollectionName);
    }

    public async Task<List<TodoItem>> GetAsync() =>
        await _todosCollection.Find(_ => true).ToListAsync();

    public async Task<TodoItem?> GetAsync(string id) =>
        await _todosCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(TodoItem newTodoItem) =>
        await _todosCollection.InsertOneAsync(newTodoItem);

    public async Task UpdateAsync(string id, TodoItem updatedTodoItem) =>
        await _todosCollection.ReplaceOneAsync(x => x.Id == id, updatedTodoItem);

    public async Task RemoveAsync(string id) =>
        await _todosCollection.DeleteOneAsync(x => x.Id == id);
}