CREATE TABLE [dbo].[StateFields]
(
	[StateId] UNIQUEIDENTIFIER NOT NULL, 
    [FieldId] UNIQUEIDENTIFIER NOT NULL,
	[Editable] BIT NOT NULL, 
    [Required] BIT NOT NULL, 
    CONSTRAINT [FK_States_Field] FOREIGN KEY ([StateId]) REFERENCES [States]([Id])
)
