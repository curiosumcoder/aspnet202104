const app = Vue.createApp({
    data() {
        return {
            filter: '',
            items: []
        }
    },
    methods: {
        search: async function () {
            console.log(`Searching for ${this.filter}`);

            let response = await fetch('js/index.json');

            if (response.status !== 200) {
                console.error(`Looks like there was a problem. Status Code: ${response.status}`);
                return;
            }

            let products = await response.json();

            let filtered = products.filter(p => p.productName.toLowerCase().indexOf(
                this.filter.toLowerCase()) !== -1);

            let grouped = filtered.reduce((result, next) => {
                let key = next.category.categoryName;
                if (!result[key]) {
                    result[key] = [];
                }
                result[key].push(next);
                return result;
            }, {});

            this.items = grouped;
            console.log(grouped);
        }
    }
});

const vm = app.mount('#app');

function noAsync() {
    fetch('js/index.json').then(
        function (response) {
            if (response.status !== 200) {
                console.log('Looks like there was a problem. Status Code: ' +
                    response.status);
                return;
            }
            response.json().then(function (data) {
                let filtered = data.filter(p => p.productName.toLowerCase().indexOf(
                    self.filter.toLowerCase()) !== -1);
                let grouped = filtered.reduce((result, next) => {
                    let key = next.category.categoryName;
                    if (!result[key]) {
                        result[key] = [];
                    }
                    result[key].push(next);
                    return result;
                }, {});
                self.items = grouped;
            });
        }
    ).catch(function (err) {
        console.log('Fetch Error :-S', err);
    });
}