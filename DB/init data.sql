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
INSERT INTO CategoryTranslations VALUES (1, 1, 'Salad', 'Salad', 'Salad', 1, 1, GETDATE(), GETDATE());
INSERT INTO CategoryTranslations VALUES (2, 1, 'Sandwich', 'Sandwich', 'Sandwich', 1, 1, GETDATE(), GETDATE());
INSERT INTO CategoryTranslations VALUES (3, 1, 'Bento', 'Bento', 'Bento', 1, 1, GETDATE(), GETDATE());

--INSERT INTO [[SubCategories]]
INSERT INTO SubCategories VALUES (1, 1, 1, 1, GETDATE(), GETDATE());
INSERT INTO SubCategories VALUES (2, 1, 1, 1, GETDATE(), GETDATE());
INSERT INTO SubCategories VALUES (3, 1, 1, 1, GETDATE(), GETDATE());

--INSERT INTO [SubCategoryTranslation]
SELECT * FROM SubCategoryTranslation
SELECT * FROM SubCategories
Select * From Products

INSERT INTO SubCategoryTranslation VALUES (1, 'Fruit Salads', 'Fruit Salads', 'Fruit Salads', 'Fruit Salads', 1, 1, GETDATE(), GETDATE());
INSERT INTO SubCategoryTranslation VALUES (1, 'Caesar Salads', 'Caesar Salads', 'Caesar Salads', 'Fruit Salads', 1, 1, GETDATE(), GETDATE());
INSERT INTO SubCategoryTranslation VALUES (1, 'Greek Salads', 'Caesar Salads', 'Caesar Salads', 'Fruit Salads', 1, 1, GETDATE(), GETDATE());

Select sub.Id, subtrans.Name, subtrans.LanguageId
From SubCategories sub, SubCategoryTranslation subtrans
Where sub.Id = subtrans.SubCategoryId

Select pro.Id, trans.Name, pro.OriginalPrice, trans.Description, subtrans.Name
From Products pro, ProductTranslations trans, SubCategoryTranslation subtrans
Where pro.Id = trans.ProductId and pro.SubCategoryId = subtrans.SubCategoryId
Select * from ProductTranslations


// Save image
if (request.ThumbnailImage != null)
{
    var thumbnailImage = await _context.ProductImages.FirstOrDefaultAsync(i => i.IsDefault == true && i.ProductId == request.Id);

    if (thumbnailImage is not null)
    {
        thumbnailImage.CreatedDate = DateTime.Now;
        thumbnailImage.FileSize = request.ThumbnailImage.Length;
        thumbnailImage.ImagePath = await SaveImage(request.ThumbnailImage);
        _context.ProductImages.Update(thumbnailImage);

    }
}
