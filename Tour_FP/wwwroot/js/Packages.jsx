// wwwroot/js/Packages.js
import React from 'react';

class Packages extends React.Component {
    render() {
        // Access the data passed from the Razor view through props
        const { data } = this.props;

        return (
            <div>
                {/* Use the data to render your React component */}
                {data.map(item => (
                    <div key={item.DestinationId}>
                        <h3>{item.CountryName}</h3>
                        <p>{item.Description}</p>
                    </div>
                ))}
            </div>
        );
    }
}

export default Packages;
