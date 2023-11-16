//app.js
const index = require('./index');
const express = require('express');
const app = express();
const port = 3000;
 
app.use(express.json());
app.get('/', (req, res) => res.json({ message: 'Funcionando!' }));
 
// GET /get-rating
app.get('/get-rating/:id', (req, res) => {
    const userId = parseInt(req.params.id);
    const rating = getRating();
    const delay = Math.floor(Math.random() * 30) * 1000;
    console.log(`Avaliação do usuário ${userId} é ${rating} (delay: ${delay}ms)`);
    setTimeout(() => {
        res.json({ userId, rating });
    }, delay); // 5000 milissegundos = 5 segundos
    
});
 
if (require.main === module) {
    //inicia o servidor
    app.listen(port)
    console.log('API funcionando!')
}

function getRating() {
    return Math.floor(Math.random() * 9);
}
 
module.exports = app