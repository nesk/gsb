CREATE TABLE FraisForfait (
  id nvarchar(3) NOT NULL,
  libelle nvarchar(20) DEFAULT NULL,
  montant decimal(5,2) DEFAULT NULL,
  PRIMARY KEY (id)
);

CREATE TABLE Etat (
  id nvarchar(2) NOT NULL,
  libelle nvarchar(30) DEFAULT NULL,
  PRIMARY KEY (id)
);

CREATE TABLE Visiteur (
  id nvarchar(4) NOT NULL,
  nom nvarchar(30) DEFAULT NULL,
  prenom nvarchar(30)  DEFAULT NULL, 
  login nvarchar(20) DEFAULT NULL,
  mdp nvarchar(20) DEFAULT NULL,
  adresse nvarchar(30) DEFAULT NULL,
  cp nvarchar(5) DEFAULT NULL,
  ville nvarchar(30) DEFAULT NULL,
  dateEmbauche date DEFAULT NULL,
  PRIMARY KEY (id)
);

CREATE TABLE FicheFrais (
  idVisiteur nvarchar(4) NOT NULL,
  mois nvarchar(6) NOT NULL,
  nbJustificatifs int DEFAULT NULL,
  montantValide decimal(10,2) DEFAULT NULL,
  dateModif date DEFAULT NULL,
  idEtat nvarchar(2) DEFAULT 'CR',
  PRIMARY KEY (idVisiteur,mois),
  FOREIGN KEY (idEtat) REFERENCES Etat(id),
  FOREIGN KEY (idVisiteur) REFERENCES Visiteur(id)
);

CREATE TABLE LigneFraisForfait (
  idVisiteur nvarchar(4) NOT NULL,
  mois nvarchar(6) NOT NULL,
  idFraisForfait nvarchar(3) NOT NULL,
  quantite int DEFAULT NULL,
  PRIMARY KEY (idVisiteur,mois,idFraisForfait),
  FOREIGN KEY (idVisiteur, mois) REFERENCES FicheFrais(idVisiteur, mois),
  FOREIGN KEY (idFraisForfait) REFERENCES FraisForfait(id)
);

CREATE TABLE LigneFraisHorsForfait (
  id int IDENTITY,
  idVisiteur nvarchar(4) NOT NULL,
  mois nvarchar(6) NOT NULL,
  libelle nvarchar(100) DEFAULT NULL,
  date date DEFAULT NULL,
  montant decimal(10,2) DEFAULT NULL,
  PRIMARY KEY (id),
  FOREIGN KEY (idVisiteur, mois) REFERENCES FicheFrais(idVisiteur, mois)
);