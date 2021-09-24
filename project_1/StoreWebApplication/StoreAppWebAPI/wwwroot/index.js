window.onload = function () {
    sessionStorage.clear();
}

const logform = document.querySelector('#loginForm');

logform.addEventListener('submit', (e) => {
    e.preventDefault();
    const username = logform.username.value;
    const password = logform.password.value;

    fetch(`/login/${username}/${password}`)
        .then(res => {
            if (!res.ok) {
                console.log(`username: ${username} password: ${password}`);
                console.log('unable to login the user');
                location.href = "register.html";
                throw new Error(`Network response was not ok (${res.status})`);
            }
            return res.json();
        })
        .then(res => {
            console.log(res);
            sessionStorage.setItem('user', JSON.stringify(res));
            console.log(sessionStorage.getItem('user'));
            //sessionStorage.clear();
            location.href = "goal.html";
        })
        .catch(err => console.log(`There was an error ${err}`));
});