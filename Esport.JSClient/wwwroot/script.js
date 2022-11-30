//http://localhost:22389/team
let Teams = [];
let connection = null;
getdata();
setupSignalR();

let TeamIdToUpdate = -1;

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:22389/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();
    connection.on("TeamCreated", (user, message) => {
        getdata();
    });
    connection.on("TeamDeleted", (user, message) => {
        getdata();
    });
    connection.on("TeamUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

async function getdata() {
    await fetch('http://localhost:22389/team')
        .then(x => x.json())
        .then(y => {
            Teams = y.$values;
            console.log(y);
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    Teams.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>"
            + t.name + "</td><td>" +
        `<button type="button" onclick="remove(${t.id})">Delete</button>` +
        `<button type="button" onclick="showupdate(${t.id})">Update</button>`
            + "</td></tr>";
    });
}

function remove(id) {
    fetch('http://localhost:22389/team/' + id, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json',
        },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

function showupdate(id) {
    console.log(document.getElementById('updateteamname').value);
    document.getElementById('updateteamname').value = Teams.find(t => t['id'] == id)['name'];
    document.getElementById('updateformdiv').style.display = 'flex';
    TeamIdToUpdate = id;
    TeamWinsToUpdate = document.getElementById('updateteamwins').value;
}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let name = document.getElementById('updateteamname').value;
    let wins = document.getElementById('updateteamwins').value;
    fetch('http://localhost:22389/team', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                Name: name, Wins: wins, ID: TeamIdToUpdate
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

function create() {
    let name = document.getElementById('teamname').value;
    fetch('http://localhost:22389/team', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                Name: name
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

