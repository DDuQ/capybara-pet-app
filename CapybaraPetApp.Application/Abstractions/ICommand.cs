namespace CapybaraPetApp.Application.Abstractions;

public interface ICommand : IBaseCommand;

//TODO: Check which commands really need to return a response.
public interface ICommand<TResponse>;

public interface IBaseCommand;
