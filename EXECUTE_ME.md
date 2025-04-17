# Guide d'exécution du projet

## Pré-requis matériels et logiciels

### Matériel :
- Raspberry Pi 4
- bouton 
- LED + résistance
- Plaque d'essai
- Câbles

### Logiciels :
- Visual Studio 2022 (Community)
- .NET SDK 8 ou 9
- Connexion SSH à la Raspberry Pi

---

##  Clonage du dépôt Git

```bash

git clone "url du dépôt"
```

### Lancer l'application Blazor Server (côté serveur)
- Lancer l’application avec Ctrl + F5 (exécution sans débogage)
- API REST exposée par défaut sur :

````bash

http://localhost:5000/api/button
````
Adapter l’URL dans le code de la Raspberry Pi 

### Dépannage
- API inaccessible	Vérifier l’adresse IP et le port
