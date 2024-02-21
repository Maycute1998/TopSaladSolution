/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [Id]
      ,[Order]
      ,[IsShow]
      ,[Status]
      ,[CreatedDate]
      ,[ModifiedDate]
  FROM [TopSalad].[dbo].[Categories]

SELECT * FROM CategoryTranslations

--INSERT INTO Languages
INSERT INTO Languages VALUES ('English', 1, 1, GETDATE(),GETDATE())
INSERT INTO Languages VALUES ('Vietnamese', 0, 1, GETDATE(),GETDATE())

--INSERT INTO Categories
INSERT INTO Categories VALUES (1, 1, 1, GETDATE(), GETDATE());
INSERT INTO Categories VALUES (2, 1, 1, GETDATE(), GETDATE());
INSERT INTO Categories VALUES (3, 1, 1, GETDATE(), GETDATE());

--INSERT INTO CategoryTranslations
INSERT INTO CategoryTranslations VALUES (1, 'Salad', 'Salad', 'Salad', 'Salad', 1, 1, GETDATE(), GETDATE());
INSERT INTO CategoryTranslations VALUES (2, 'Sandwich', 'Sandwich', 'Sandwich', 'Sandwich', 1, 1, GETDATE(), GETDATE());
INSERT INTO CategoryTranslations VALUES (3, 'Bento', 'Bento', 'Bento', 'Bento', 1, 1, GETDATE(), GETDATE());

--INSERT INTO [[SubCategories]]
INSERT INTO SubCategories VALUES (1, 1, 1, 1, GETDATE(), GETDATE());
INSERT INTO SubCategories VALUES (2, 1, 1, 1, GETDATE(), GETDATE());
INSERT INTO SubCategories VALUES (3, 1, 1, 1, GETDATE(), GETDATE());
INSERT INTO SubCategories VALUES (4, 1, 1, 1, GETDATE(), GETDATE());

  --INSERT INTO [SubCategoryTranslation]
  SELECT * FROM SubCategoryTranslation
  SELECT * FROM SubCategories
  SELECT * FROM Categories
  SELECT * FROM CategoryTranslations
  Select * From Products

INSERT INTO SubCategoryTranslation VALUES (1, 'Fruit Salads', 'Fruit Salads', 'Fruit Salads', 'Fruit Salads', 1, 1, GETDATE(), GETDATE());
INSERT INTO SubCategoryTranslation VALUES (1, 'Caesar Salads', 'Caesar Salads', 'Caesar Salads', 'Fruit Salads', 1, 1, GETDATE(), GETDATE());
INSERT INTO SubCategoryTranslation VALUES (1, 'Greek Salads', 'Caesar Salads', 'Caesar Salads', 'Fruit Salads', 1, 1, GETDATE(), GETDATE());
INSERT INTO SubCategoryTranslation VALUES (1002, 'Avocado Salads', 'This show-stopping summer salad puts creamy avocado, juicy tomatoes, and crisp cucumbers front and center.', 'Broccoli salad', 'Broccoli salad', 1, 1, GETDATE(), GETDATE());
INSERT INTO SubCategoryTranslation VALUES (1003, 'Broccoli salad', 'Another new variety which many love is the broccoli quinoa salad.', 'Broccoli salad', 'Broccoli salad', 1, 1, GETDATE(), GETDATE());
INSERT INTO SubCategoryTranslation VALUES (1004, 'Seafood salad', 'Another new variety which many love is the Seafood salad.', 'Seafood salad', 'Seafood salad', 1, 1, GETDATE(), GETDATE());


--INSERT INTO [dbo].[Products]
INSERT INTO [dbo].[Products] VALUES (1002, 0, 13000, 5, 1, GETDATE(), GETDATE());
INSERT INTO [dbo].[Products] VALUES (1002, 0, 16000, 7, 1, GETDATE(), GETDATE());
INSERT INTO [dbo].[Products] VALUES (1, 0, 13000, 4, 1, GETDATE(), GETDATE());
INSERT INTO [dbo].[ProductTranslations] VALUES (1002, 'Classic Broccoli Salad', 'This creamy Broccoli Cauliflower Salad with bacon and pecans is healthy, satisfying and great for potlucks.','This creamy Broccoli Cauliflower Salad with bacon and pecans is healthy, satisfying and great for potlucks.','Classic Broccoli Salad','Classic Broccoli Salad',1,1,GETDATE(), GETDATE());
INSERT INTO [dbo].[ProductTranslations] VALUES (1003, 'Creamy Cheddar Broccoli Salad', 'A sweet and tangy dressing makes this creamy cheddar broccoli salad a huge hit wherever it goes.','A sweet and tangy dressing makes this creamy cheddar broccoli salad a huge hit wherever it goes.','Creamy Cheddar Broccoli Salad','Creamy Cheddar Broccoli Salad',1,1,GETDATE(), GETDATE());
INSERT INTO [dbo].[ProductTranslations] VALUES (1004, 'Mixed Berry Salad with Goat Cheese and Balsamic Vinegar', 'Fresh summer berries are tossed with goat cheese and balsamic vinegar in this perfectly balanced sweet and salty mixed berry salad.','Mixed Berry Salad with Goat Cheese and Balsamic Vinegar','Mixed Berry Salad with Goat Cheese and Balsamic Vinegar',1,1,GETDATE(), GETDATE());

  Select * From Products
  Select * From ProductTranslations

Select sub.Id, subtrans.Name, subtrans.LanguageId
From SubCategories sub, SubCategoryTranslation subtrans
Where sub.Id = subtrans.SubCategoryId

Select pro.Id, trans.Name, pro.OriginalPrice, trans.Description, subtrans.Name
From Products pro, ProductTranslations trans, SubCategoryTranslation subtrans
Where pro.Id = trans.ProductId and pro.SubCategoryId = subtrans.SubCategoryId
Select * from ProductTranslations