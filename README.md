# Netflix Web for Xbox — proef 0.1
Onafhankelijke, onofficiële UWP-wrapper voor de officiële HTTPS-website van Netflix. Geen Netflix-logo, geen eigen accountformulier, geen advertentie- of DRM-omzeiling, geen analytics, geen externe proxy. Alleen navigatie naar netflix.com en subdomeinen; andere top-level navigatie wordt geblokkeerd. Inloggen gebeurt rechtstreeks bij Netflix. WebView2-gebruik is experimenteel; Netflix heeft deze omgeving niet gecertificeerd.

## Build
De aparte Windows-workflow bouwt en ondertekent een x64-pakket met een uitsluitend voor dit project gemaakte testcertificaat. Download het workflowartifact NetflixWebXbox-x64. Installeer via Xbox Device Portal met de meegeleverde app, certificaat en x64-afhankelijkheden. Geen bestaande app verwijderen.

## Hardwaretest
1. Installeer naast bestaande apps. Start in Dev Home.
2. Controleer of de officiële website laadt en de controllerpointer op links kan klikken.
3. Log alleen in op de echte Netflix-pagina als die correct wordt weergegeven.
4. Kies een titel. Verifieer beeld én geluid; test pauze, hervatten en volledig scherm. Menu of B kan de werkbalk terughalen (controllergedrag moet worden getest).
5. Als Netflix het apparaat/browser/DRM weigert: noteer uitsluitend de zichtbare foutcode, nooit credentials of cookies. Gebruik de officiële Retail-app wanneer nodig. Geen DRM-omzeiling.
6. Als WebView2 niet start, probeer de knop Open browser (alleen indien de Xbox een browser aanbiedt).

## Status
Experimental pilot; not affiliated with or endorsed by Netflix. No playback compatibility claim until verified on hardware. Build artifacts expire after three days.
