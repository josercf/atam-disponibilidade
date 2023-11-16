import http from 'k6/http';
import { sleep } from 'k6';

export let options = {
    duration: '30s',
    vus: 10,
};

export default function() {
    let res = http.get('http://localhost:80');
    console.log(`Response time was ${res.timings.duration} ms`);
    sleep(1);
}