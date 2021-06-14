# Why doesn't lazy loading work?

##Before Clearing LazyLoadingEnabled=True:
```
-------
ParticipantList 'I'm a P List' Id=1
        1: 123@gmail.com
        2: 456@gmail.com
-------

Clearing Change Tracker
via LazyLoading LazyLoadingEnabled=True
-------
ParticipantList 'I'm a P List' Id=1
:-(
-------

After Also selecting emails from DB LazyLoadingEnabled=True
-------
ParticipantList 'I'm a P List' Id=1
:-(
-------

Clearing Change Tracker
via Include LazyLoadingEnabled=True
-------
ParticipantList 'I'm a P List' Id=1
        1: 123@gmail.com
        2: 456@gmail.com
-------
```

