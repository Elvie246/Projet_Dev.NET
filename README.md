# Projet_Dev.NET
## Objectif du projet

Ce projet vise à créer une communication fluide entre un dispositif embarqué (Raspberry Pi 4), une API REST en .NET Core, et une interface utilisateur web développée avec Blazor Server. L’utilisateur peut visualiser les événements physiques (appui bouton) captés depuis la Raspberry Pi, recevoir des notifications en temps réel et allumer une LED .

---

## Organisation du projet

Le projet est divisé en deux dossiers principaux :

- `BlazorApp/` : contient tout le code relatif à l’application web réalisée avec **Blazor**. On y retrouve :
  - Le client web
  - Le serveur (API REST)
  - Les focntionnalités principales
- `RaspberryPi/` : contient le code destiné à être exécuté sur la **Raspberry Pi ** pour gérer :
  - La communication avec le serveur Blazor
  - Le contrôle de la LED


 ---

## Fonctionnalités principales

### Côté Raspberry Pi (Application Console .NET)
- Détection d’un appui sur un bouton GPIO.
- Transmission de l’événement à un serveur Blazor via une API REST.

### Côté Serveur (Blazor Server .NET)
- API REST pour réception des événements.
- Interface Blazor pour l’affichage des notifications.

---

## Architecture du projet

Raspberry Pi 4-App Console (.NET) ---> GPIO (entrée bouton, sortie LED) - Appels POST ---> API REST

Blazor Server (Web)-API REST Controller  ---> Interface dynamique Blazor (SignalR) ---> Journalisation 


---

## Technologies utilisées

| Élément                   | Technologie utilisée      |
|---------------------------|---------------------------|
| Dispositif embarqué       | Raspberry Pi 4            |
| App console .NET          | .NET  + System.Device.Gpio |
| Interface web             | Blazor Server             |
| API REST                  | ASP.NET Core              |
| Notifications             | SignalR / Composants Blazor |
| Logs                      |                      |

---

## Fonctionnalités supplémentaires implémentées

- **Mode "Ne pas déranger"** 
- **Journalisation** 


---

## Auteurs

- Elvie MAKANGA
- Samuel AKAKPO



