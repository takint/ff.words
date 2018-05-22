export class AppUtils {
    public static apiHost: string = window['ffwordsAppData'].apiEndpoint; //'http://277ce74a.ngrok.io/api/';

    public static isNullOrEmpty(input: any): boolean {
        // Null or empty
        if (input === null || input === undefined || input === '') {
            return true;
        }

        // Array empty
        if (typeof input.length === 'number' && typeof input !== 'function') {
            return !input.length;
        }

        // Blank string like '   '
        if (typeof input === 'string' && input.match(/\S/) === null) {
            return true;
        }

        return false;
    }
}
