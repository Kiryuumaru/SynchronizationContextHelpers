﻿using System;
using System.Threading.Tasks;

namespace SynchronizationContextHelpers;

/// <summary>
/// Provides operations for <see cref="SyncOperation"/> with proper disposable implementations.
/// </summary>
public class SyncContext
{
    #region Properties

    /// <summary>
    /// Gets the <see cref="SyncOperation"/> used by this object.
    /// </summary>
    public SyncOperation SyncOperation { get; private set; }

    #endregion

    #region Initializers

    /// <summary>
    /// Creates new instance of the <c>SyncContext</c> class.
    /// </summary>
    /// <remarks>
    /// <para>To use safely in UI operations, create the instance in UI thread.</para>
    /// <para>See <see cref="SyncOperation"/></para>
    /// </remarks>
    public SyncContext()
    {
        SyncOperation = new SyncOperation();
    }

    #endregion

    #region Methods

    /// <summary>
    /// Executes <paramref name="action"/> to the current synchronization context without blocking the executing thread.
    /// </summary>
    /// <param name="action">
    /// The action to be executed at the current synchronization context.
    /// </param>
    /// <param name="parameters">
    /// The parameters to be pass at the current synchronization context.
    /// </param>
    protected void ContextPost(Action action, params object[] parameters)
    {
        SyncOperation.ContextPost(action, parameters);
    }

    /// <summary>
    /// Executes <paramref name="action"/> to the current synchronization context and blocking the executing thread until the <paramref name="action"/> ended.
    /// </summary>
    /// <param name="action">
    /// The action to be executed at the current synchronization context.
    /// </param>
    /// <param name="parameters">
    /// The parameters to be pass at the current synchronization context.
    /// </param>
    protected void ContextSend(Action action, params object[] parameters)
    {
        SyncOperation.ContextSend(action, parameters);
    }

    /// <summary>
    /// Executes <paramref name="action"/> to the current synchronization context asynchronously.
    /// </summary>
    /// <param name="action">
    /// The action to be executed at the current synchronization context.
    /// </param>
    /// <param name="parameters">
    /// The parameters to be pass at the current synchronization context.
    /// </param>
    /// <returns>
    /// A <see cref="Task"/> that represents a proxy for the task returned by <paramref name="action"/>.
    /// </returns>
    protected async Task ContextSendAsync(Action action, params object[] parameters)
    {
        await SyncOperation.ContextSendAsync(action, parameters);
    }

    /// <summary>
    /// Executes <paramref name="func"/> to the current synchronization context asynchronously.
    /// </summary>
    /// <param name="func">
    /// The action to be executed at the current synchronization context.
    /// </param>
    /// <param name="parameters">
    /// The parameters to be pass at the current synchronization context.
    /// </param>
    /// <returns>
    /// A <see cref="Task"/> that represents a proxy for the task returned by <paramref name="func"/>.
    /// </returns>
    protected async Task ContextSendAsync(Func<Task> func, params object[] parameters)
    {
        await SyncOperation.ContextSendAsync(func, parameters);
    }

    #endregion
}
