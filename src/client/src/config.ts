import {default as envConfig} from './config.json';

const envAlias: any = {
    local: 'local'
}

export function getConfig() {
    let env: string;

    if (process.env.REACT_APP_ENV === undefined) {
        env = 'local'
    }
    else {
        env = process.env.REACT_APP_ENV; 
    }

    //let config: { [key: string]: {[key: string]: string }} = envConfig[envAlias[env]];
    //let urls: { [key: string]: string} =  
        let config: any = { envConfig[envAlias[env]] }

    return {
        urls: config.urls 
    }
}
