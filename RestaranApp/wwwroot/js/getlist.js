let userlist = async function () {
    const responce = await fetch("/users")
    if (!responce.ok) {
        throw new Error(`error: ${response.status}`);
    }

    const users = await responce.json()
    const container = document.createElement('div');
    container.className = 'users-container';

    users.forEach(user => {
        const user_elem = document.createElement('div');
        user_elem.className = 'user-block';

        user_elem.innerHTML = `
                    <div>
                        <p class="list-elem">UUID: ${user.uuid}</p>
                        <p class="list-elem">Name: ${user.name}</p>
                        <p class="list-elem">Status: ${user.status}</p>
                    </div>
                `;

        container.appendChild(user_elem);
    });

    document.body.appendChild(container);
}

let restaranlist = async function () {
    const responce = await fetch("/restarans")
    if (!responce.ok) {
        throw new Error(`error: ${response.status}`);
    }

    const restarans = await responce.json()
    const container = document.createElement('div');
    container.className = 'restarans-container';

    restarans.forEach(restaran => {
        const restaran_elem = document.createElement('div');
        restaran_elem.className = 'restaran-block';

        restaran_elem.innerHTML = `
                    <div>
                        <p class="list-elem">UUID: ${restaran.uuid}</p>
                        <p class="list-elem">Name: ${restaran.name}</p>
                        <p class="list-elem">Capacity: ${restaran.capacity}</p>
                    </div>
                `;

        container.appendChild(restaran_elem);
    });

    document.body.appendChild(container);
}

let orderlist = async function () {
    const responce = await fetch("/restarans")
    if (!responce.ok) {
        throw new Error(`error: ${response.status}`)
    }

    const restarans = await responce.json()

    const rcon = document.createElement('div')
    rcon.className = 'orders-container'

    restarans.forEach(async (r) => {
        console.log(`/orders/rid/${r.uuid}`)
        const orders_res = await fetch(`/orders/rid/${r.uuid}`)
        if (!orders_res.ok) {
            throw new Error(`error: ${orders_res.status}`)
        }

        const orders = await orders_res.json()

        const ocon = document.createElement('div')
        ocon.className = 'orders'

        const r_elem = document.createElement('div');
        r_elem.className = 'r-block';
        r_elem.innerHTML = `<h1>${r.name}</h1>`

        orders.forEach(order => {
            const order_elem = document.createElement('div');
            order_elem.className = 'order-block';

            order_elem.innerHTML = `
                    <div>
                        <p class="list-elem">UUID: ${order.uuid}</p>
                        <p class="list-elem">StartTime: ${order.startTime}</p>
                        <p class="list-elem">EndTime: ${order.endTime}</p>
                    </div>
                `;

            ocon.appendChild(order_elem);
        })

        rcon.appendChild(r_elem)
        rcon.appendChild(ocon);
    })

    document.body.appendChild(rcon)
}