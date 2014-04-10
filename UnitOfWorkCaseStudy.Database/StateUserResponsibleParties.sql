CREATE TABLE [dbo].[StateUserResponsibleParties]
(
	[StateId] UNIQUEIDENTIFIER NOT NULL , 
    [UserId] UNIQUEIDENTIFIER NOT NULL
	CONSTRAINT [FK_States_UserResponsibleParty] FOREIGN KEY ([StateId]) REFERENCES [States]([Id])
)
