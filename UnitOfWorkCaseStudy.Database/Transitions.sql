﻿CREATE TABLE [dbo].[Transitions]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [WorkflowId] UNIQUEIDENTIFIER NOT NULL, 
    [FromStateId] UNIQUEIDENTIFIER NOT NULL, 
    [ToStateId] UNIQUEIDENTIFIER NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_Transitions_Workflows] FOREIGN KEY ([WorkflowId]) REFERENCES [Workflows]([Id])
)
