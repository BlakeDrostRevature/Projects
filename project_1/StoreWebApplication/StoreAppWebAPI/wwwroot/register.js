const rForm = document.getElementById("registerform");
var listOfCustomers;

rForm.addEventListener('submit', (e) => {
    e.preventDefault();
    const fname = rForm.fname.value;
    const lname = rForm.lname.value;
    const uname = rForm.username.value;
    const pword = rForm.password.value;

    fetch(`/customerlist`)
        .then(res => {
            if (!res.ok) {
                console.log('unable to fetch all the users');
                throw new Error(`Network response was not ok (${res.status})`);
            }
            return res.json();
        })
        .then(res => {
            console.log(res);
            listOfCustomers = res;
            let success = true;
            for (let i = 0; i < listOfCustomers.length; i++) {
                if (uname == listOfCustomers[i].username) {
                    success = false;
                }
            }
            if (success) {
                fetch(`/register/${fname}/${lname}/${uname}/${pword}`, {
                    method: 'POST'
                    })
                    .then(res => {
                        console.log(res);
                        console.log(JSON.stringify(res));
                        return res.json();
                    })
                    .then(data => {
                        sessionStorage.setItem('user', JSON.stringify(data));
                        location.href = "goal.html";
                    })
            }
        })
        .catch(err => console.log(`There was an error ${err}`));
    
    
})