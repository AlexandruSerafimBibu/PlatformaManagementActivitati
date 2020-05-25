# PlatformaManagementActivitati

Aceasta platforma este destinata oamenilor care vor sa isi gestioneze proiectele mai usor. Un user neinregistrat poate vedea doar pagina de start si pagina de login.

Dupa crearea unui nou cont poti:

    - vedea lista de proiecte
    - crea un proiect nou
    - edita/sterge proiectele create de tine
    - crea/edita/sterge echipele din cadrul proiectelor tale
    - crea/edita/sterge task-uri din cadrul proiectelor tale
    - membrii unei echipe pot modifica statusul unui task de care sunt responsabili
  
Admnistratorul are acces la tot ce cuprinde aplicatia si se ocupa de buna functionare a acesteia. Poate sterge task-uri, echipe sau proiecte.

Pentru baza de date s-a folosit abordarea code-first folosind migratii, iar pentru transmiterea de date in view-uri s-au folosit viewModels, nu viewBag-uri.
