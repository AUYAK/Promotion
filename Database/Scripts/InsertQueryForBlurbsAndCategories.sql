Use PromotionDB

--Delete From Blurbs;
--Delete From BlurbCategories;

Insert Into BlurbCategories(Name,Description)
Values('Car','Blurbs about car'),('Web sites','Blurbs about sites'),('Goods for journeys','Blurbs of goods for tourism');

Insert Into Blurbs(Name,Description,CategoryID)
Values('Lada showroom in Luninets','New showroom for farmers',(Select CategoryID From BlurbCategories Where Name='Car')),
('Opel showroom in the capital of Belarus','Showroom for citiziens of capital',(Select CategoryID From BlurbCategories Where Name='Car')),
('Lamba showroom in Vegas','Showroom for men in white-color T-shirt',(Select CategoryID From BlurbCategories Where Name='Car')),
('Adidas','Shoes for activities',(Select CategoryID From BlurbCategories Where Name='Goods for journeys')),
('Obey','Caps for russian teenagers',(Select CategoryID From BlurbCategories Where Name='Goods for journeys')),
('Vk.com','Mail.ru group',(Select CategoryID From BlurbCategories Where Name='Goods for journeys')),
('Kufar.by','The most popular internet showcase in Belarussia',(Select CategoryID From BlurbCategories Where Name='Web sites')),
('Aliexpress','The most popular internet showcase in the World',(Select CategoryID From BlurbCategories Where Name='Web sites')),
('E-bay','The most popular  site for auctions in the World',(Select CategoryID From BlurbCategories Where Name='Web sites')),
('Goodom.com','This web site will be the most popular blurbroom',(Select CategoryID From BlurbCategories Where Name='Web sites'));
